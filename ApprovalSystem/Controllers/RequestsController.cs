using ApprovalSystem.Dtos;
using ApprovalSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApprovalSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestsController : ControllerBase
{
    private readonly ILogger<RequestsController> _logger;
    private ISharePointService _sharePointService;

    public RequestsController(ILogger<RequestsController> logger, ISharePointService sharePointService)
    {
        _logger = logger;
        _sharePointService = sharePointService;
    }

    /// <summary>
    /// Creates a new approval request
    /// </summary>
    /// <param name="requestDto">The request object</param>
    /// <returns>The created request ID</returns>
    /// <response code="200">Request created successfully</response>
    /// <response code="400">Invalid request data</response>
    [HttpPost]
    public async Task<ActionResult> AddRequest(RequestDto requestDto)
    {
        try
        {
            var requestsId = await _sharePointService.AddItemToRequestsList(requestDto);
            return requestsId is null ? BadRequest(new { message = "Failed to create request" }) : Ok(new { requestsId, message = "success" });
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Update the final status of specified request
    /// </summary>
    /// <param name="requestId">The request ID</param>
    /// <param name="requestStatus">The request details object</param>
    [HttpPost("{requestId}/status")]
    public ActionResult GetRequests(string requestId, RequestStatusDto requestStatus)
    {
        return Ok(new { requestId, requestStatus });
    }
}