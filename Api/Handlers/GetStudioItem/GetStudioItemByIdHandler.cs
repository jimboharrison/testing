using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeoplesPartnership.ApiRefactor.Database;
using PeoplesPartnership.ApiRefactor.DTOs.Responses;

namespace PeoplesPartnership.ApiRefactor.Handlers.GetStudioItem;

public class GetStudioItemByIdHandler : IGetStudioItemByIdHandler
{
    private readonly IMapper _mapper;
    private readonly StudioContext _studioContext;

    public GetStudioItemByIdHandler(IMapper mapper, StudioContext studioContext)
    {
        _mapper = mapper;
        _studioContext = studioContext;
    }

    public async Task<ServiceResponse<GetStudioItemDto>> GetStudioItemById(int id)
    {
        var item = await _studioContext.StudioItems
            .Where(item => item.StudioItemId == id)
            .Include(type => type.StudioItemType)
            .FirstOrDefaultAsync();

        if (item == null)
        {
            return new ServiceResponse<GetStudioItemDto>
            {
                Success = false,
                Type = ServiceResponseType.NotFound,
                Message = "Studio Item not found"
            };
        }

        var serviceResponse = new ServiceResponse<GetStudioItemDto>
        {
            Data = _mapper.Map<GetStudioItemDto>(item),
            Message = "Here's your selected studio item",
            Success = true
        };
        return serviceResponse;
    }
}