namespace LibraryApp.Models.Abstract
{
    public interface IBookTypeRepository : IRepository<BookType>
    {
        void Update(BookType bookType);
        void Save();
    }
}
