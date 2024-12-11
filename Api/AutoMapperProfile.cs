using PeoplesPartnership.ApiRefactor.DTOs;
using AutoMapper;
using PeoplesPartnership.ApiRefactor.Database.Models;
using PeoplesPartnership.ApiRefactor.DTOs.Requests;
using PeoplesPartnership.ApiRefactor.DTOs.Responses;

namespace PeoplesPartnership.ApiRefactor
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StudioItem, GetStudioItemDto>();
            CreateMap<AddStudioItemDto, StudioItem>();
            CreateMap<StudioItem, GetStudioItemHeaderDto>();
            CreateMap<StudioItemType, GetStudioItemTypeDto>();
        }
    }
}