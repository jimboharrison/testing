using System.Collections.Generic;
using System.Threading.Tasks;
using PeoplesPartnership.ApiRefactor.DTOs.Responses;

namespace PeoplesPartnership.ApiRefactor.Handlers.GetAllStudioItemTypes;

public interface IGetAllStudioItemTypesHandler
{
    Task<ServiceResponse<List<GetStudioItemTypeDto>>> GetAllStudioItemTypes();
}