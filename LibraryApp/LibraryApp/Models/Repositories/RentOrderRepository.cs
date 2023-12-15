using LibraryApp.Models.Abstract;
using LibraryApp.Models.Concrete;
using LibraryApp.Utility;
using NuGet.Protocol.Core.Types;
using System.Linq.Expressions;

namespace LibraryApp.Models.Repositories
{
    public class RentOrderRepository : Repository<RentOrder>, IRentOrderRepository
    {
        private LibraryDbContext _context;
        public RentOrderRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
            
        }

        public void Save()
        {
           _context.SaveChanges();
        }

        public void Update(RentOrder rentorder)
        {
            _context.Update(rentorder);
        }
    };
}
