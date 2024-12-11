using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeoplesPartnership.ApiRefactor.Database;
using PeoplesPartnership.ApiRefactor.DTOs;
using PeoplesPartnership.ApiRefactor.DTOs.Responses;

namespace PeoplesPartnership.ApiRefactor.Handlers.GetAllStudioHeaderItems;

public class GetAllStudioHeaderItemsHandler : IGetAllStudioHeaderItemsHandler
{
    private readonly IMapper _mapper;
    private readonly StudioContext _studioContext;

    public GetAllStudioHeaderItemsHandler(IMapper mapper, StudioContext studioContext)
    {
        _mapper = mapper;
        _studioContext = studioContext;
    }

    public async Task<ServiceResponse<List<GetStudioItemHeaderDto>>> GetAllStudioHeaderItems()
    {
        var itemHeaders = await _studioContext
            .StudioItems
            .Select(c => _mapper.Map<GetStudioItemHeaderDto>(c))
            .ToListAsync();
        
        var serviceResponse = new ServiceResponse<List<GetStudioItemHeaderDto>>
        {
            Data = itemHeaders,
            Message = "Here's all the items in your studio",
            Success = true
        };

        return serviceResponse;
    }
}