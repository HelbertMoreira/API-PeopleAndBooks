using PeopleAndBooks.DataConverter.VO;
using PeopleAndBooks.Model;
using PeopleAndBooks.Repository.User;
using PeopleAndBooks.System.Configuration.Token;
using PeopleAndBooks.System.Configuration.Token.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PeopleAndBooks.Business.Implementations
{
    public class LoginBusinessImplementationcs : ILogin
    {
        
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;

        private IUserRepository _repository;
        private readonly TokenService _tokenService;

        public LoginBusinessImplementationcs(TokenConfiguration configuration, IUserRepository repository, TokenService tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserSystemVO userCredentials)
        {
            var user = _repository.ValidateCredentials(userCredentials);
            if (user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.Token = refreshToken;
            user.RefreshToken = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            return AtualizaUserSystem(user, accessToken, refreshToken);      
        }

        public TokenVO ValidateCredentials(TokenVO tokenVO)
        {
            var accessToken = tokenVO.AccessToken;
            var refreshToken = tokenVO.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var userName = principal.Identity.Name;

            var user = _repository.ValidateCredentials(userName);

            if (user == null || user.Token != refreshToken || user.RefreshToken <= DateTime.Now) return null;

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.Token = refreshToken;

            return AtualizaUserSystem(user, accessToken, refreshToken);

        }

        public bool RvokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }

        private TokenVO AtualizaUserSystem(UserSystem user, string accessToken, string refreshToken)
        {
            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }

        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);   
        }
    }
}
