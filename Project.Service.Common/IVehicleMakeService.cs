using Project.Common;
using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service.Common
{
    public interface IVehicleMakeService
    {
        Task<IVehicleMake> GetMakeAsync(Guid id);
        Task<IEnumerable<IVehicleMakeBase>> GetAllMakeAsync();
        Task<int> UpdateMakeAsync(IVehicleMake make);
        Task<IVehicleMake> AddMakeAsync(IVehicleMake make);
        Task<int> RemoveMakeAsync(Guid id);
        Task<IPagedList<IVehicleMake>> FindMakeAsync(IFilteringParams filter, ISortingParams sorter, IPagingParams pager);
    }
}
