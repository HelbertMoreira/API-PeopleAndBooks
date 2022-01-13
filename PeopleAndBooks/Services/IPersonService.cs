using PeopleAndBooks.Model;
using System.Collections.Generic;

namespace PeopleAndBooks.Services
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person Update(Person person);
        List<Person> FindAll();
        Person FindById(int id);
        void Delete(int id);
    }
}
