using PeopleAndBooks.Model;
using PeopleAndBooks.Repository;
using System.Collections.Generic;

/*
    CAMADA DE NEGÓCIOS
    Classe implementada para que esta receba regras de negócios quando assim houver.
 */

namespace PeopleAndBooks.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        #region Injeção de dependência
        private readonly IPersonRepositoy _repository;
        public PersonBusinessImplementation(IPersonRepositoy repository)
        {
            _repository = repository;
        }
        #endregion

        #region Métodos
        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        public Person FindById(int id)
        {
            return _repository.FindById(id);
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
        #endregion
    }
}
