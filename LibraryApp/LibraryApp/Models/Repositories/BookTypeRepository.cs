using LibraryApp.Utility;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Models.Abstract;

namespace LibraryApp.Models.Concrete
{
    public class BookTypeRepository : Repository<BookType>, IBookTypeRepository
    {
        private LibraryDbContext _context;
        public BookTypeRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(BookType bookType)
        {
            _context.Update(bookType);
        }
    }
}
