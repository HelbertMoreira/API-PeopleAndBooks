using PeopleAndBooks.Data;
using PeopleAndBooks.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PeopleAndBooks.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private readonly SqlServerContext _context;
        
        public PersonServiceImplementation(SqlServerContext context)
        {
            _context = context; 
        }
        
        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
                return person;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            var result = _context.Person.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Person.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }

        public List<Person> FindAll()
        {
            return _context.Person.ToList();
        }

        public Person FindById(int id)
        {
            return _context.Person.SingleOrDefault(x => x.Id.Equals(id));
        }              

        public Person Update(Person person)
        {
            if (!Exists(person)) return new Person();
            
            var result = _context.Person.SingleOrDefault(p => p.Id.Equals(person.Id));
            if (result == null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            return person;
        }

        private bool Exists(Person person)
        {
            return _context.Person.Any(x => x.Id.Equals(person.Id));
        }
    }
}
