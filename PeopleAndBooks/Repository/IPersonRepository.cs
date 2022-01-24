using PeopleAndBooks.Model;
using PeopleAndBooks.Repository.Generic;
using System.Collections.Generic;

namespace PeopleAndBooks.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person DisableOrEnable(int id);
        List<Person> FindByName(string nome, string sobrenome);
    }
}
