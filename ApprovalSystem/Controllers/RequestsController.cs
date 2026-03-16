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

    [HttpPost]
    public async Task<ActionResult> AddRequest(RequestDto requestDto)
    {
        try
        {
            await _sharePointService.AddItemToRequestsList(requestDto);
            return Ok();
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("{requestId}/status")]
    public ActionResult GetRequests(string requestId, RequestStatusDto requestStatus)
    {
        return Ok(new { requestId, requestStatus });
    }
}