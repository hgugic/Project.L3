using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Common;
using Project.DAL;
using Project.DAL.Entities;
using Project.Model.Common;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Repository
{
    public class VehicleModelRepository : Repository<IVehicleModel, VehicleModel>, IVehicleModelRepository
    {
        private readonly DataContext context;

        public VehicleModelRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
        }

        public async Task<IPagedList<IVehicleModel>> FindModelAsync(IFilteringParams filter, ISortingParams sorter, IPagingParams pager)
        {
            var makers = await context.Models.Where(m => string.IsNullOrEmpty(filter.Search) ? m != null : m.Name.ToLower().Contains(filter.Search.ToLower()))
                                              .Where(m => filter.MakeId == Guid.Empty ? m.MakeId != Guid.Empty : m.MakeId == filter.MakeId).Include("Make")
                                              .OrderByDescending(x => sorter.SortDirection == "dsc" ? x.Name : "")
                                              .OrderBy(x => string.IsNullOrEmpty(sorter.SortDirection) || sorter.SortDirection == "asc" ? x.Name : "").ToListAsync();


            return await mapper.Map<IEnumerable<IVehicleModel>>(makers).ToPagedListAsync(pager.CurrentPage, pager.PageSize);
        }
    }
}
