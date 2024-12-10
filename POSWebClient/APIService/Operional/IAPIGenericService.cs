namespace POSWebClient.APIService.Operional
{
    public interface IAPIGenericService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Create(T item);
        T Update(T item);
        T Delete(int id);



    }

}
