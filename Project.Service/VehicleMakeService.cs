using Project.Model.Common;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using Project.Service.Common;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using Project.Common;
using AutoMapper;

namespace Project.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IUnitOfWork unitOfWork;

        public VehicleMakeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IVehicleMake> AddMakeAsync(IVehicleMake make)
        {
            var createdMaker = await unitOfWork.Makers.AddAsync(make);
            await unitOfWork.CommitAsync();
            return createdMaker;
        }

        public async Task<IEnumerable<IVehicleMakeBase>> GetAllMakeAsync()
        {
            return await unitOfWork.Makers.GetMakerBaseAsync();
        }


        public async Task<IVehicleMake> GetMakeAsync(Guid id)
        {
            return await unitOfWork.Makers.GetAsync(id);
        }

        public async Task<int> RemoveMakeAsync(Guid id)
        {
            var result = await unitOfWork.Makers.RemoveAsync(id);
            await unitOfWork.CommitAsync();
            return result;
        }

        public async Task<int> UpdateMakeAsync(IVehicleMake make)
        {
            await unitOfWork.Makers.UpdateAsync(make);
            return await unitOfWork.CommitAsync();
        }

        public async Task<IPagedList<IVehicleMake>> FindMakeAsync(IFilteringParams filter, ISortingParams sorter, IPagingParams pager)
        {
            return await unitOfWork.Makers.FindMakeAsync(filter, sorter, pager);
        }
    }
}
