using CQRSApp.Domain;
using CQRSApp.Repositories.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSApp.Persistance.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly CQRSAppDBContext _context;
        internal DbSet<TEntity> _dbSet;
        public GenericRepository(CQRSAppDBContext context)
        {
            _context = context;
            _dbSet = this._context.Set<TEntity>();
        }
        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
