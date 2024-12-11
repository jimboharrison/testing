using System.Threading.Tasks;
using PeoplesPartnership.ApiRefactor.DTOs.Responses;

namespace PeoplesPartnership.ApiRefactor.Handlers.GetStudioItem;

public interface IGetStudioItemByIdHandler
{
    Task<ServiceResponse<GetStudioItemDto>> GetStudioItemById(int id);
}