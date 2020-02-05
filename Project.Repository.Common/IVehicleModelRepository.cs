using Project.Common;
using Project.DAL.Entities;
using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Repository.Common
{
    public interface IVehicleModelRepository : IRepository<IVehicleModel, VehicleModel>
    {
        Task<IPagedList<IVehicleModel>> FindModelAsync(IFilteringParams filter, ISortingParams sorter, IPagingParams pager);
    }
}
