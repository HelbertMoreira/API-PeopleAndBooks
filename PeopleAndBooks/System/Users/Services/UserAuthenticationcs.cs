using PeopleAndBooks.Data;
using PeopleAndBooks.Model;
using System.Linq;
using System;

namespace PeopleAndBooks.System.Users.Services
{
    public class UserAuthenticationcs
    {

        private readonly SqlServerContext _context;

        public UserAuthenticationcs(SqlServerContext context)
        {
            _context = context;
        }

        public UserSystem RefreshUserInfo(UserSystem user)
        {
            if (!Exists(user)) return null;

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return user;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        private bool Exists(UserSystem user)
        {
            return _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id)).ToString() != String.Empty ? true : false;
            
        }
    }
}
