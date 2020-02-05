using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public class VehicleMake : IVehicleMake
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public DateTime Founded { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Logo { get; set; }
    }
}
