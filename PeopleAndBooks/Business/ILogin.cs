using PeopleAndBooks.DataConverter.VO;

namespace PeopleAndBooks.Business
{
    public interface ILogin
    {
        TokenVO ValidateCredentials(UserSystemVO user);
    }
}
