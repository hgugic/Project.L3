using AutoMapper;
using Project.Model;
using Project.Model.Common;
using Project.WebAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.WebAPI.Mappings
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
