using System.Linq.Expressions;

namespace LibraryApp.Models.Abstract
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeprops = null);

        T Get(Expression<Func<T, bool>> filter, string? includeprops = null);
        void Add(T entity);
        void Delete(T entity);
        void DeleteInterval(IEnumerable<T> entities);
    }
}
