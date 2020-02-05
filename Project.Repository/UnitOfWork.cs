using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;

        public UnitOfWork(DataContext context, IVehicleMakeRepository makeRepository, IVehicleModelRepository modelRepository)
        {
            this.context = context;
            Makers = makeRepository;
            Models = modelRepository;
        }
        public IVehicleMakeRepository Makers { get; }

        public IVehicleModelRepository Models { get; }

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();           
        }

    }
}
