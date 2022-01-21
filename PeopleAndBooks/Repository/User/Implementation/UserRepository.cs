using PeopleAndBooks.Data;
using PeopleAndBooks.DataConverter.VO;
using PeopleAndBooks.Model;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PeopleAndBooks.Repository.User.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlServerContext _context;

        public UserRepository(SqlServerContext context)
        {
            _context = context;
        }

        public UserSystem ValidateCredentials(UserSystemVO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());            
            return _context.Users.FirstOrDefault(u => (u.Login == user.Login) && (u.Senha == pass));            
        }

        public UserSystem ValidateCredentials(string userName)
        {
            return _context.Users.FirstOrDefault(u => (u.Login == userName));
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => (u.Login == userName));
            if (user is null) return false;

            user.Token = null;
            _context.SaveChanges();
            return true;
        }

        public UserSystem RefreshUserInfo(UserSystem user)
        {
            if(!_context.Users.Any(x => x.Id.Equals(user.Id))) return null;

            var result = _context.Users.SingleOrDefault(x => x.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}
