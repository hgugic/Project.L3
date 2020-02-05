using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model.Common
{
    public interface IVehicleMakeBase
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
