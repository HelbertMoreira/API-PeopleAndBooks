using PeopleAndBooks.Data;
using PeopleAndBooks.Model;
using System;
using System.Linq;

namespace PeopleAndBooks.Repository.Generic
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(SqlServerContext context) : base(context) { }

        public Person DisableOrEnable(int id)
        {
            if (!_context.Person.Any(p => p.Id.Equals(id))) return null;
            var person = _context.Person.SingleOrDefault(p => p.Id.Equals(id));
            if (person != null)
            {
                if (person.Ativo == false)
                    person.Ativo = true;
                else
                    person.Ativo = false;

                try
                {
                    _context.Entry(person).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                    return person;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return person;
        }
    }
}
