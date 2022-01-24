using PeopleAndBooks.Data;
using PeopleAndBooks.Model;
using System;
using System.Collections.Generic;
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

        public List<Person> FindByName(string nome, string sobrenome)
        {
            if (!string.IsNullOrWhiteSpace(nome) && !string.IsNullOrWhiteSpace(sobrenome))
            {
                return _context.Person.Where(
                    p => p.First_Name.Contains(nome) && 
                    p.Last_Name.Contains(sobrenome)).ToList();
            }
            else if (string.IsNullOrWhiteSpace(nome) && !string.IsNullOrWhiteSpace(sobrenome))
            {
                return _context.Person.Where(
                    p => p.Last_Name.Contains(sobrenome)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(nome) && string.IsNullOrWhiteSpace(sobrenome))
            {
                return _context.Person.Where(
                    p => p.First_Name.Contains(nome)).ToList();
            }
            else return null;
        }
    }
}
