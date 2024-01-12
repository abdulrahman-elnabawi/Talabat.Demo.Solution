using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
                return (IReadOnlyList<T>)await _context.Set<Product>().Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).ToListAsync();


        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
           => await ApplySpecification(spec).FirstOrDefaultAsync();


        public async Task<int> GetCountAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).CountAsync();

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

        public async Task AddAsync(T entity)
        => await _context.Set<T>().AddAsync(entity);
        
        public void Update(T entity)
        => _context.Set<T>().Update(entity);

        public void Delete(T entity)
        => _context.Set<T>().Remove(entity);
        
    }
}
