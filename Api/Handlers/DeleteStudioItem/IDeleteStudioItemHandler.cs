using System.Threading.Tasks;
using PeoplesPartnership.ApiRefactor.DTOs.Responses;

namespace PeoplesPartnership.ApiRefactor.Handlers.DeleteStudioItem;

public interface IDeleteStudioItemHandler
{
    Task<ServiceResponse<GetStudioItemDto>> DeleteStudioItem(int id);
}