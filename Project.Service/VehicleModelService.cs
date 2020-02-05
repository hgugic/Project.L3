using Project.Common;
using Project.Model.Common;
using Project.Repository.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IUnitOfWork unitOfWork;

        public VehicleModelService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IVehicleModel> AddModelAsync(IVehicleModel model)
        {
            var createdModel = await unitOfWork.Models.AddAsync(model);
            await unitOfWork.CommitAsync();
            return createdModel;
        }

        public async Task<IPagedList<IVehicleModel>> FindModelAsync(IFilteringParams filter, ISortingParams sorter, IPagingParams pager)
        {
            return await unitOfWork.Models.FindModelAsync(filter, sorter, pager);
        }

        public async Task<IEnumerable<IVehicleModel>> GetAllModelsAsync()
        {
            return await unitOfWork.Models.GetAllAsync();
        }

        public async Task<IVehicleModel> GetModelAsync(Guid id)
        {
            return await unitOfWork.Models.GetAsync(id);
        }

        public async Task<int> RemoveModelAsync(Guid id)
        {
            var result = await unitOfWork.Models.RemoveAsync(id);
            await unitOfWork.CommitAsync();
            return result;
        }

        public async Task<int> UpdateModelAsync(IVehicleModel model)
        {
            await unitOfWork.Models.UpdateAsync(model);
            return await unitOfWork.CommitAsync();
        }
    }
}
