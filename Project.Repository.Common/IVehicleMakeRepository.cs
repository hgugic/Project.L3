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
    public interface IVehicleMakeRepository : IRepository<IVehicleMake, VehicleMake>
    {
        Task<IEnumerable<IVehicleMakeBase>> GetMakerBaseAsync();
        Task<IPagedList<IVehicleMake>> FindMakeAsync(IFilteringParams filter, ISortingParams sorter, IPagingParams pager);
    }
}
