using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeoplesPartnership.ApiRefactor.Database;
using PeoplesPartnership.ApiRefactor.Database.Models;
using PeoplesPartnership.ApiRefactor.DTOs;
using PeoplesPartnership.ApiRefactor.DTOs.Requests;
using PeoplesPartnership.ApiRefactor.DTOs.Responses;

namespace PeoplesPartnership.ApiRefactor.Handlers.AddStudioItem;

public class AddStudioItemHandler : IAddStudioItemHandler
{
    private readonly IMapper _mapper;
    private readonly StudioContext _studioContext;

    public AddStudioItemHandler(IMapper mapper, StudioContext studioContext)
    {
        _mapper = mapper;
        _studioContext = studioContext;
    }

    public async Task<ServiceResponse<GetStudioItemDto>> AddStudioItem(AddStudioItemDto newStudioItem)
    {
        var studioItemType =
            await _studioContext.StudioItemTypes.SingleOrDefaultAsync(x => x.StudioItemTypeId == newStudioItem.StudioItemTypeId);

        if (studioItemType == null)
            return new ServiceResponse<GetStudioItemDto>
            {
                Data = null, 
                Success = false, 
                Type = ServiceResponseType.BadRequest,
                Message = "Invalid studio item type"
            };

        var item = _mapper.Map<StudioItem>(newStudioItem);
        item.StudioItemType = studioItemType;
        await _studioContext.StudioItems.AddAsync(item);
        await _studioContext.SaveChangesAsync();

        var serviceResponse = new ServiceResponse<GetStudioItemDto>
        {
            Data = _mapper.Map<GetStudioItemDto>(item),
            Message = $"New item added.  Id: {item.StudioItemId}",
            Success = true
        };

        return serviceResponse;
    }
}