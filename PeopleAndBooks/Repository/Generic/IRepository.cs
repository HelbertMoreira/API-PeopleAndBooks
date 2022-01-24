using PeopleAndBooks.Model.Base;
using System.Collections.Generic;

namespace PeopleAndBooks.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T Update(T item);
        List<T> FindAll();
        T FindById(int id);
        void Delete(int id);
        bool Exists(T item);
        List<T> FindWithPagedShearch(string query);
        int GetCount(string query);
    }
}
