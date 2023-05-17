
using BaseNetCore.Utils;
using BaseNetCore.Infrastructure.Helper;
using BaseNetCore.Infrastructure.Helper.Constant;
using BaseNetCore.Infrastructure.Schemas;
using BaseNetCore.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BaseNetCore.UseCase.AuthUseCase
{
 
    public class AuthFlow 
    {
        readonly IUserService userService; 
        public AuthFlow(IUserService _service)
        {
            userService = _service; 
        }

        public Response Login(string username, string password)
        {
            Response response = userService.GetUser(username);
            if (response.Status == Message.ERROR)
            {
                return response;
            }
            User user = (User)response.Result;
            Response res = userService.Compare(user.Password, password);
            if (res.Status == Message.ERROR)
            {
                return response;
            }
            string accessToken = Jwt.GenerateAccessToken(user.Id, Jwt.SECRET_KEY);
            string refreshToken = Jwt.GenerateRefreshToken();
            userService.SetRefreshToken(refreshToken, user.Id);
            userService.UpdateLoginTime(user.Id);

            return new Response(Message.SUCCESS, new TokenPresenter { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        public Response RefreshToken(string accessToken, string refreshToken)
        {
            var key = Encoding.ASCII.GetBytes(Jwt.SECRET_KEY);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);
            var userCredentialString = jwtToken.Claims.First(x => x.Type == "id").Value;
            int userId = Int32.Parse(userCredentialString);
            Response response = userService.Get(userId);
            User user = (User)response.Result;
            bool isMatched = user.HashRefreshToken.Equals(refreshToken);
            if (isMatched)
            {
                var newToken = Jwt.GenerateAccessToken(userId, Jwt.SECRET_KEY);
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
