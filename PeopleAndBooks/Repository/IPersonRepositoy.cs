using PeopleAndBooks.Model;
using System.Collections.Generic;

namespace PeopleAndBooks.Repository
{
    public interface IPersonRepositoy
    {
        Person Create(Person person);
        Person Update(Person person);
        List<Person> FindAll();
        Person FindById(int id);
        void Delete(int id);
        bool Exists(Person person);
    }
}
