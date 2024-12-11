using System.Collections.Generic;
using System.Threading.Tasks;
using PeoplesPartnership.ApiRefactor.DTOs.Responses;

namespace PeoplesPartnership.ApiRefactor.Handlers.GetAllStudioHeaderItems;

public interface IGetAllStudioHeaderItemsHandler
{
    Task<ServiceResponse<List<GetStudioItemHeaderDto>>> GetAllStudioHeaderItems();
}