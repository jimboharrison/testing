using System.Threading.Tasks;
using PeoplesPartnership.ApiRefactor.DTOs.Requests;
using PeoplesPartnership.ApiRefactor.DTOs.Responses;

namespace PeoplesPartnership.ApiRefactor.Handlers.UpdateStudioItem;

public interface IUpdateStudioItemHandler
{
     Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItem(int studioItemId, UpdateStudioItemDto updatedStudioItem);
}