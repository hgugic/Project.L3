using AutoMapper;
using Project.DAL.Entities;
using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Repository.Mappings
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IVehicleMake, VehicleMake>();
            CreateMap<VehicleMake, IVehicleMake>();

            CreateMap<IVehicleModel, VehicleModel>();
            CreateMap<VehicleModel, IVehicleModel>();


        }
    }
}
