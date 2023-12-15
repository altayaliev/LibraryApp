using LibraryApp.Models.Abstract;
using LibraryApp.Models.Concrete;
using LibraryApp.Utility;

namespace LibraryApp.Models.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private LibraryDbContext _context;
        public BookRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            _context.Update(book);
        }
    }
}
