namespace MSCustomerAPI.DAL
{
    public interface IGenericRespository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Add(T entity);
        int Update(T entity);
        int Delete(int id);
    }
}
