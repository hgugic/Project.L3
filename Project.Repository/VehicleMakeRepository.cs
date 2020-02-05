using AutoMapper;
using Project.Common;
using Project.DAL;
using Project.DAL.Entities;
using Project.Model;
using Project.Model.Common;
using Project.Repository.Common;
using Project.Repository.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Repository
{
    public class VehicleMakeRepository : Repository<IVehicleMake, DAL.Entities.VehicleMake>, IVehicleMakeRepository
    {
        private readonly DataContext context;

        public VehicleMakeRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
        }


        public async Task<IPagedList<IVehicleMake>> FindMakeAsync(IFilteringParams filter, ISortingParams sorter, IPagingParams pager)
        {
            var makers = await context.Makers
                .Where(m => string.IsNullOrEmpty(filter.Search) ? m != null : m.Name.ToLower().Contains(filter.Search.ToLower()))
                .OrderByDescending(x => sorter.SortDirection == "dsc" ? x.Name : "")
                .OrderBy(x => string.IsNullOrEmpty(sorter.SortDirection) || sorter.SortDirection == "asc" ? x.Name : "")
                .ToListAsync();


            return await mapper.Map<IEnumerable<IVehicleMake>>(makers).ToPagedListAsync(pager.CurrentPage, pager.PageSize);

        }

        public async Task<IEnumerable<IVehicleMakeBase>> GetMakerBaseAsync()
        {
            return mapper.Map<IEnumerable<IVehicleMakeBase>>(await context.Makers.OrderBy(v => v.Name)
                                .Select(x => new VehicleMakeBase() { Id = x.Id, Name = x.Name }).ToListAsync());
        }


    }
}
