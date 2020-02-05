using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IUnitOfWork
    {
        IVehicleMakeRepository Makers { get; }
        IVehicleModelRepository Models { get; }
        Task<int> CommitAsync();
    }
}
