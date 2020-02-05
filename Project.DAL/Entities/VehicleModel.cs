using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.Entities
{
    public class VehicleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public DateTime Year { get; set; }
        public string Type { get; set; }
        public string Engine { get; set; }
        public string Description { get; set; }
        public Guid MakeId { get; set; }
        public VehicleMake Make { get; set; }
    }
}
