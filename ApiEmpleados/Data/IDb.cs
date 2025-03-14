namespace ApiEmpleados.Data
{
    public interface IDb<T>
    {
        public Repository<T> GetRepo();
    }
}
