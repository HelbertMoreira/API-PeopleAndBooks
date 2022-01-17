using PeopleAndBooks.DataConverter.Converter.Contract;
using PeopleAndBooks.DataConverter.Converter.VO;
using PeopleAndBooks.Model;
using System.Collections.Generic;
using System.Linq;

namespace PeopleAndBooks.DataConverter.Converter.Implementation
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    { 
        //Nesse método temos apenas o parse de um objeto para outro
        //Só lembrando que temos bibliotecas que fazer isso, mas a idéia aqui é mostrar como implementamos...
        public Person Parse(PersonVO origin)
        {
            if (origin == null) return null;
            return new Person
            {
                Id = origin.Id,
                First_Name = origin.First_Name,
                Last_Name = origin.Last_Name,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public PersonVO Parse(Person origin)
        {
            if (origin == null) return null;
            return new PersonVO
            {
                Id = origin.Id,
                First_Name = origin.First_Name,
                Last_Name = origin.Last_Name,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null) return null;
            //Aqui se pega cada item da lista e "parsea" para o objeto desejado.
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
