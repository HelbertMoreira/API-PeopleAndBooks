using PeopleAndBooks.DataConverter.VO;
using PeopleAndBooks.Model;

namespace PeopleAndBooks.Repository.User
{
    public interface IUserRepository
    {
        UserSystem ValidateCredentials(UserSystemVO user);
        UserSystem ValidateCredentials(string userName);
        UserSystem RefreshUserInfo(UserSystem user);
        bool RevokeToken(string userName);
    }
}
