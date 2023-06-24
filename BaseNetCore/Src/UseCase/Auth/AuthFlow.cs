
using BaseNetCore.Src.Utils;
using BaseNetCore.Src.Helper;
using BaseNetCore.Src.Helper.Constant;
using BaseNetCore.Src.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BaseNetCore.Src.Services.Schemas;

namespace BaseNetCore.Src.UseCase.Auth
{
    public class AuthFlow
    {
        readonly IUser userService;
        readonly IAuth authService;
        public AuthFlow(IUser _userService, IAuth _authService)
        {
            userService = _userService;
            authService = _authService;
        }

        public Response Login(string username, string password)
        {
            Response response = userService.GetUser(username);
            if (response.Status == Message.ERROR)
            {
                return response; 
            }
            UserSchema user = (UserSchema)response.Result;
            bool isMatched = JwtUtil.Compare(password, user.Password);
            if (!isMatched)
            {
                return new Response(Message.ERROR, new { });
            }
            string accessToken = JwtUtil.GenerateAccessToken(user.Id);
            string refreshToken = JwtUtil.GenerateRefreshToken();
            authService.SetRefreshToken(refreshToken, user.Id);
            authService.UpdateLoginTime(user.Id);
            return new Response(Message.SUCCESS, new TokenPresenter { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        public Response RefreshToken(string accessToken, string refreshToken)
        {
            var key = Encoding.ASCII.GetBytes(JwtUtil.SECRET_KEY);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);
            var userCredentialString = jwtToken.Claims.First(x => x.Type == "id").Value;
            int userId = Int32.Parse(userCredentialString);
            Response response = userService.Get(userId);
            UserSchema user = (UserSchema)response.Result;
            bool isMatched = user.HashRefreshToken.Equals(refreshToken);
            if (isMatched)
            {
                var newToken = JwtUtil.GenerateAccessToken(userId);
                var newRefreshToken = JwtUtil.GenerateRefreshToken();

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
