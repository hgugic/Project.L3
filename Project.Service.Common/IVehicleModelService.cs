using Project.Common;
using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service.Common
{
    public interface IVehicleModelService
    {
        Task<IVehicleModel> GetModelAsync(Guid id);
        Task<IEnumerable<IVehicleModel>> GetAllModelsAsync();
        Task<int> UpdateModelAsync(IVehicleModel model);
        Task<IVehicleModel> AddModelAsync(IVehicleModel model);
        Task<int> RemoveModelAsync(Guid id);
        Task<IPagedList<IVehicleModel>> FindModelAsync(IFilteringParams filter, ISortingParams sorter, IPagingParams pager);
    }
}
