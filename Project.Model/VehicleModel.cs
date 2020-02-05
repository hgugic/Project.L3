using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public class VehicleModel : IVehicleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public DateTime Year { get; set; }
        public string Type { get; set; }
        public string Engine { get; set; }
        public string Description { get; set; }
        public Guid MakeId { get; set; }
        public IVehicleMake Make { get; set; }
    }
}
