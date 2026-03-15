using ApprovalSystem.Dtos;

namespace ApprovalSystem.Interfaces;

public interface ISharePointService
{
    Task<string> AddItemToRequestsList(RequestDto request);
}
