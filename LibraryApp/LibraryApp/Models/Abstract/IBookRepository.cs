namespace LibraryApp.Models.Abstract
{
    public interface IBookRepository:IRepository<Book>
    {
        void Update(Book book);
        void Save();
    }
}
