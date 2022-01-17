using PeopleAndBooks.Model;
using System.Collections.Generic;

namespace PeopleAndBooks.Business
{
    public interface IBookBusiness
    {
        Book Create(Book book);
        Book Update(Book book);
        List<Book> FindAll();
        Book FindById(int id);
        void Delete(int id);
    }
}
