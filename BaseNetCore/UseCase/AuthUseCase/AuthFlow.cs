
using BaseNetCore.Utils;
using BaseNetCore.Infrastructure.Helper;
using BaseNetCore.Infrastructure.Helper.Constant;
using BaseNetCore.Infrastructure.Schemas;
using BaseNetCore.Services;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BaseNetCore.UseCase.AuthUseCase
{
    public interface IAuthFlow
    {
        Response Login(string username, string password);
        Response RefreshToken(string accessToken, string refreshToken);
    }

    public class AuthFlow : IAuthFlow
    {
        readonly IUserService userService;
        private readonly AppSettings appSettings;
        public AuthFlow(IUserService _service, IOptions<AppSettings> _appSettings)
        {
            userService = _service;
            appSettings = _appSettings.Value;
        }

        public Response Login(string username, string password)
        {
            Response response = userService.GetUser(username);
            if (response.Status == Message.ERROR)
            {
                // throw new UnauthorizedException("Invalid account or password");
            }
            User user = (User)response.Result;
            Response res = userService.Compare(user.Password, password);
            if (res.Status == Message.ERROR)
            {
                // throw new UnauthorizedException("Invalid account or password");
            }
            string accessToken = Jwt.GenerateAccessToken(user.Id, appSettings.Secret);
            string refreshToken = Jwt.GenerateRefreshToken();
            userService.SetRefreshToken(refreshToken, user.Id);
            userService.UpdateLoginTime(user.Id);

            return new Response(Message.SUCCESS, new TokenPresenter { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        public Response RefreshToken(string accessToken, string refreshToken)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);
            var userCredentialString = jwtToken.Claims.First(x => x.Type == "id").Value;
            int userId = Int32.Parse(userCredentialString);
            Response response = userService.Get(userId);
            User user = (User)response.Result;
            bool isMatched = user.HashRefreshToken.Equals(refreshToken);
            if (isMatched)
            {
                var newToken = Jwt.GenerateAccessToken(userId, appSettings.Secret);
                var newRefreshToken = Jwt.GenerateRefreshToken();

                return new Response(Message.SUCCESS, new
                {
                    AccessToken = newToken,
                    RefreshToken = newRefreshToken
                });
            }
            return new Response(Message.ERROR, new { });
        }

    }
}
