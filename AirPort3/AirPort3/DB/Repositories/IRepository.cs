namespace AirPort3.DB.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);
        IQueryable<T> GetAll();
        T? Get(int id);
        IEnumerable<T> GetLast(int number);

    }
}
