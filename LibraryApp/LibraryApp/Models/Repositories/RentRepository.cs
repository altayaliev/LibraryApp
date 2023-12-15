using LibraryApp.Models.Abstract;
using LibraryApp.Models.Concrete;
using LibraryApp.Utility;

namespace LibraryApp.Models.Repositories
{
    public class RentRepository : Repository<Rent>, IRentRepository
    {
        private LibraryDbContext _context;
        public RentRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Rent rent)
        {
            _context.Update(rent);
        }
    }
}
