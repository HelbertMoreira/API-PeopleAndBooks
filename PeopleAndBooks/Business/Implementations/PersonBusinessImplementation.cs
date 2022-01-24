using PeopleAndBooks.DataConverter.Converter.Implementation;
using PeopleAndBooks.DataConverter.Converter.VO;
using PeopleAndBooks.Model;
using PeopleAndBooks.Repository;
using PeopleAndBooks.Repository.Generic;
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
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;
        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }
        #endregion

        #region Métodos
        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindById(int id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public List<PersonVO> FindByName(string nome, string sobrenome)
        {
            return _converter.Parse(_repository.FindByName(nome, sobrenome));
        }

        public PersonVO Create(PersonVO person)
        {
            //Converte PersonVO recebido como parâmetro...
            var personEntity = _converter.Parse(person);
            //Persiste Person na base de dados...
            personEntity = _repository.Create(personEntity); 
            //Devolde o objeto persistido na base.
            return _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }
        public PersonVO DisableOrEnable(int id)
        {
            var personEntity = _repository.DisableOrEnable(id);
            return _converter.Parse(personEntity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }


        #endregion
    }
}
