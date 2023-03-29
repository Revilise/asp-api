namespace WebAPI.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        T Delete(int id);
    }
}
