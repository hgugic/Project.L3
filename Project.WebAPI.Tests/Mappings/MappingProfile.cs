using AutoMapper;
using Project.Model.Common;
using Project.Model;
using Project.WebAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace Project.WebAPI.Tests.Mappings
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IVehicleMake, VehicleMakeDto>();
            CreateMap<VehicleMakeDto, IVehicleMake>();

            CreateMap<VehicleMake, VehicleMakeDto>();
            CreateMap<VehicleMakeDto, VehicleMake>();

            CreateMap<IVehicleModel, VehicleModelDto>().ForMember(dest => dest.Make, opt =>
    opt.MapFrom(src => src.Make.Name));
            CreateMap<VehicleModelDto, IVehicleModel>();

            CreateMap<VehicleModel, VehicleModelDto>();
            CreateMap<VehicleModelDto, VehicleModel>();

            CreateMap<IPagedList<IVehicleModel>, IPagedList<VehicleModelDto>>();

            CreateMap<IVehicleMakeBase, VehicleMakeBaseDto>();
            CreateMap<VehicleMakeBaseDto, IVehicleMakeBase>();

        }
    }
}
