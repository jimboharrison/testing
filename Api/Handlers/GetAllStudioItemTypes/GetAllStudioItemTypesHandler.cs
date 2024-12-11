using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeoplesPartnership.ApiRefactor.Database;
using PeoplesPartnership.ApiRefactor.DTOs.Responses;

namespace PeoplesPartnership.ApiRefactor.Handlers.GetAllStudioItemTypes;

public class GetAllStudioItemTypesHandler : IGetAllStudioItemTypesHandler
{
    private readonly IMapper _mapper;
    private readonly StudioContext _studioContext;

    public GetAllStudioItemTypesHandler(IMapper mapper, StudioContext studioContext)
    {
        _mapper = mapper;
        _studioContext = studioContext;
    }

    public async Task<ServiceResponse<List<GetStudioItemTypeDto>>> GetAllStudioItemTypes()
    {
        var studioItemTypes = await _studioContext
            .StudioItemTypes
            .OrderBy(s => s.Value)
            .ToListAsync();
        
        var serviceResponse = new ServiceResponse<List<GetStudioItemTypeDto>>
        {
            Data = studioItemTypes.Select(x => _mapper.Map<GetStudioItemTypeDto>(x)).ToList(),
            Message = "Item types fetched",
            Success = true
        };

        return serviceResponse;
    }
}