using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PeoplesPartnership.ApiRefactor.Database;
using PeoplesPartnership.ApiRefactor.Database.Models;
using PeoplesPartnership.ApiRefactor.DTOs;
using PeoplesPartnership.ApiRefactor.DTOs.Responses;

namespace PeoplesPartnership.ApiRefactor.Handlers.DeleteStudioItem;

public class DeleteStudioItemHandler : IDeleteStudioItemHandler
{
    private readonly IMapper _mapper;
    private readonly StudioContext _studioContext;

    public DeleteStudioItemHandler(IMapper mapper, StudioContext studioContext)
    {
        _mapper = mapper;
        _studioContext = studioContext;
    }

    public async Task<ServiceResponse<GetStudioItemDto>> DeleteStudioItem(int id)
    {
            var serviceResponse = new ServiceResponse<GetStudioItemDto>();
            
            var item = await _studioContext.StudioItems.SingleOrDefaultAsync(c => c.StudioItemId == id);

            if (item == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Type = ServiceResponseType.NotFound;
                serviceResponse.Message = "No studio item found";
                return serviceResponse;
            }
            
            _studioContext.Remove(item);
            await _studioContext.SaveChangesAsync();

            serviceResponse.Data =  _mapper.Map<GetStudioItemDto>(item);
            serviceResponse.Success = true;
            serviceResponse.Message = "Item deleted";

            return serviceResponse;
    }
}