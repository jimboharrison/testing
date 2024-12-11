using System.Threading.Tasks;
using PeoplesPartnership.ApiRefactor.DTOs.Requests;
using PeoplesPartnership.ApiRefactor.DTOs.Responses;

namespace PeoplesPartnership.ApiRefactor.Handlers.AddStudioItem;

public interface IAddStudioItemHandler
{
    Task<ServiceResponse<GetStudioItemDto>> AddStudioItem(AddStudioItemDto newStudioItem);

}