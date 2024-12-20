﻿using Cursive.Domain.Entities.Abstractions;
using Cursive.Domain.Repositories.Interfaces;
using Cursive.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Cursive.Infra.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        public Repository(CursiveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly CursiveDbContext _dbContext;

        public IQueryable<TEntity> All => _dbContext.Set<TEntity>();

        public IQueryable<TEntity> AllNotTracking => _dbContext.Set<TEntity>().AsNoTracking();


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await All.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllNotTrackingAsync()
        {
            return await AllNotTracking.ToListAsync();    
        }

        public async Task<TEntity?> GetAsync(Predicate<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().FindAsync(predicate);
        }

        public Task<TEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;  
        }

        public async Task SaveAsync(TEntity entity)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
