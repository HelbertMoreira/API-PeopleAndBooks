using PeopleAndBooks.DataConverter.Converter.VO;
using System.Collections.Generic;

namespace PeopleAndBooks.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO Update(PersonVO person);
        List<PersonVO> FindAll();
        PersonVO FindById(int id);
        void Delete(int id);
    }
}
