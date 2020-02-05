using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Common
{
    public interface IVehicleMake
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
        DateTime Founded { get; set; }
        string City { get; set; }
        string Country { get; set; }
        string Logo { get; set; }
    }
}
