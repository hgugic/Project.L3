using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Dtos
{
    public class VehicleMakeDto
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
