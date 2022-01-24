using PeopleAndBooks.Model;
using PeopleAndBooks.Repository.Generic;

namespace PeopleAndBooks.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person DisableOrEnable(int id);
    }
}
