using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public class VehicleMakeBase : IVehicleMakeBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
