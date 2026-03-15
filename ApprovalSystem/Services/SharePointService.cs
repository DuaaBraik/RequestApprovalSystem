using ApprovalSystem.Dtos;
using System.Text;

using System.Net.Http.Headers;
using System.Text.Json;
using Mapster;
using ApprovalSystem.Models;
using ApprovalSystem.Interfaces;
using System.Security.Cryptography.X509Certificates;

using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System;

namespace ApprovalSystem.Services;

public class SharePointService : ISharePointService
{
    private SharePointConfig? _spConfig;
    private string certificatePath;

    public SharePointService(IConfiguration configuration)
    {
        _spConfig = configuration.GetSection("SharePoint").Get<SharePointConfig>();
        certificatePath = Path.Combine(Directory.GetCurrentDirectory(), "Certificates", _spConfig?.CertificateFileName);
    }

    public async Task<string> AddItemToRequestsList(RequestDto request)
    {
        var sharePointRequest = request.Adapt<SharePointRequestModel>();

        sharePointRequest.RequestId = Guid.NewGuid().ToString();

        var json = JsonSerializer.Serialize(sharePointRequest);

        var certificate = new X509Certificate2(certificatePath, _spConfig?.CertificatePassword);

        var app = ConfidentialClientApplicationBuilder.Create(_spConfig?.ClientId)
            .WithCertificate(certificate)
            .WithAuthority(new Uri($"{_spConfig?.LoginUrl}/{_spConfig?.TenantId}"))
            .Build();

        string[] scopes = new[] { _spConfig?.BaseUrl };
        var authResult = await app.AcquireTokenForClient(scopes).ExecuteAsync();

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");

        var url = $"{_spConfig?.SiteUrl}/_api/web/lists/GetByTitle('ApprovalRequests')/items";

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        await httpClient.PostAsync(url, content);

        return sharePointRequest.RequestId;
    }
}

