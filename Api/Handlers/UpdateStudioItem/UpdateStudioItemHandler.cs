using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeoplesPartnership.ApiRefactor.Database;
using PeoplesPartnership.ApiRefactor.DTOs.Requests;
using PeoplesPartnership.ApiRefactor.DTOs.Responses;

namespace PeoplesPartnership.ApiRefactor.Handlers.UpdateStudioItem;

public class UpdateStudioItemHandler : IUpdateStudioItemHandler
{
    private readonly IMapper _mapper;
    private readonly StudioContext _studioContext;

    public UpdateStudioItemHandler(IMapper mapper, StudioContext studioContext)
    {
        _mapper = mapper;
        _studioContext = studioContext;
    }

    public async Task<ServiceResponse<GetStudioItemDto>> UpdateStudioItem(int studioItemId, UpdateStudioItemDto updatedStudioItem)
    {
        var serviceResponse = new ServiceResponse<GetStudioItemDto>();

        var studioItem = await _studioContext.StudioItems
            .SingleOrDefaultAsync(c => c.StudioItemId == studioItemId);

        if (studioItem == null)
        {
            serviceResponse.Type = ServiceResponseType.NotFound;
            serviceResponse.Message = "No studio item found";
            return serviceResponse;
        }

        var studioItemType = await _studioContext.StudioItemTypes
            .FirstOrDefaultAsync(c => c.StudioItemTypeId == updatedStudioItem.StudioItemTypeId);

        if (studioItemType == null)
        {
            serviceResponse.Type = ServiceResponseType.BadRequest;
            serviceResponse.Message = "Invalid studio item type";
            return serviceResponse;
        }

        try
        {
            studioItem.Acquired = updatedStudioItem.Acquired;
            studioItem.Description = updatedStudioItem.Description;
            studioItem.Eurorack = updatedStudioItem.Eurorack;
            studioItem.Name = updatedStudioItem.Name;
            studioItem.Price = updatedStudioItem.Price;
            studioItem.SerialNumber = updatedStudioItem.SerialNumber;
            studioItem.Sold = updatedStudioItem.Sold;
            studioItem.SoldFor = updatedStudioItem.SoldFor;
            studioItem.StudioItemType = studioItemType;
            
            _studioContext.StudioItems.Update(studioItem);
            await _studioContext.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetStudioItemDto>(studioItem);
            serviceResponse.Message = "Update successful";
            serviceResponse.Success = true;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}