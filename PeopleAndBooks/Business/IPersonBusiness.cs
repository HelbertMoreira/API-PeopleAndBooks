using PeopleAndBooks.DataConverter.Converter.VO;
using PeopleAndBooks.DataConverter.Utils;
using System.Collections.Generic;

namespace PeopleAndBooks.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO Update(PersonVO person);
        List<PersonVO> FindAll();
        PersonVO FindById(int id);
        List<PersonVO> FindByName(string nome, string sobrenome);
        void Delete(int id);
        PersonVO DisableOrEnable(int id);

        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDiretion, int pageSize, int currentPage);
    }
}
