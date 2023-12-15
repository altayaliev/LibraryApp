namespace LibraryApp.Models.Abstract
{
    public interface IRentOrderRepository:IRepository<RentOrder>
    {
        void Update(RentOrder rentorder);
        void Save();
    }
}
