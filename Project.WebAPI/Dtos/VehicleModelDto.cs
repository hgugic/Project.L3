using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Dtos
{
    public class VehicleModelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public DateTime Year { get; set; }
        public string Type { get; set; }
        public string Engine { get; set; }
        public string Description { get; set; }
        public Guid MakeId { get; set; }
        public string Make { get; set; }
    }
}
