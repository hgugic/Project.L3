using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Common
{
    public interface IVehicleModel
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
        DateTime Year { get; set; }
        string Type { get; set; }
        string Engine { get; set; }
        string Description { get; set; }
        Guid MakeId { get; set; }
        IVehicleMake Make { get; set; }
    }
}
