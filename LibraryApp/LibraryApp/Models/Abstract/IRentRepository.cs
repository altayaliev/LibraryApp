namespace LibraryApp.Models.Abstract
{
    public interface IRentRepository:IRepository<Rent>
    {

        void Update(Rent rent);
        void Save();
    }
}
