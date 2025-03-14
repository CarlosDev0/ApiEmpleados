using System.Collections.Generic;

namespace ApiEmpleados.Data
{
    public class Repository<T>
    {
        Dictionary<Guid, List<T>> repo;
        public Repository()
        {
            repo = new Dictionary<Guid, List<T>>();
        }
        public void Set(Guid key, T item)
        {
            var list = new List<T>();

            var result = repo.TryGetValue(key, out list);
            if (result)
            {
                if (list == null)
                   list = new List<T>();
                list.Add(item);
            }
            else { 
                repo[key] = new List<T>();
                repo[key].Add(item);
            }
        }
        public async Task<List<T>> Get(Guid key)
        {
            return await Task.FromResult(repo.TryGetValue(key, out var list) ? list : new List<T>());
        }
    }
}
