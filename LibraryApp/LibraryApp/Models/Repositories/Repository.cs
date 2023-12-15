using LibraryApp.Models.Abstract;
using LibraryApp.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryApp.Models.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private LibraryDbContext _context;
        internal DbSet<T> dbSet;

        public Repository(LibraryDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
           // _context.Books.Include(k => k.BookType);
            
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteInterval(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeprops = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeprops))
            {
                foreach (var includeprop in includeprops.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeprops=null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeprops))
            {
                foreach (var includeprop in includeprops.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            }
             return query.ToList();
        }
    }
}
