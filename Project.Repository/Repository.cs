using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public abstract class Repository<T, TEntity> : IRepository<T, TEntity> where T : class where TEntity : class
    {
        protected readonly DbContext Context;
        protected IMapper mapper;

        public Repository(DbContext context, IMapper mapper)
        {
            this.Context = context;
            this.mapper = mapper;

        }

        public async Task<T> AddAsync(T vehicle)
        {
            var entity = mapper.Map<TEntity>(vehicle);
            await Context.Set<TEntity>().AddAsync(entity);
            return mapper.Map<T>(entity);
        }

        public async Task<int> UpdateAsync(T vehicle)
        {            
            try
            {
                var entity = mapper.Map<TEntity>(vehicle);
                var dbEntityEntry = Context.Entry(entity);
                dbEntityEntry.State = EntityState.Modified;
                return await Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<T>>(await Context.Set<TEntity>().ToListAsync());
        }

        public async Task<T> GetAsync(Guid id)
        {
            var make = await Context.Set<TEntity>().FindAsync(id);
            Context.Entry(make).State = EntityState.Detached;
            return mapper.Map<T>(make);
        }

        public async Task<int> RemoveAsync(Guid id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            Context.Remove(entity);
            return await Task.FromResult(1);
        }
    }
}
