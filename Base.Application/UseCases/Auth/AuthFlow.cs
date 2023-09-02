using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Base.Utils;
using Base.Core.Schemas;
using Base.Services.Base;
using Base.Services;

namespace Base.Application.UseCases
{
    public class AuthFlow
    {
        private readonly IUnitOfWork uow;
        public AuthFlow(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public Response Login(string username, string password)
        {
            List<UserSchema> users = uow.Users.Get(username);

            UserSchema user = users.FirstOrDefault();
            bool isMatched = JwtUtil.Compare(password, user.Password);
            if (!isMatched)
            {
                return new Response(Message.ERROR, new { });
            }
            string accessToken = JwtUtil.GenerateAccessToken(user.Id);
            string refreshToken = JwtUtil.GenerateRefreshToken();
            uow.Users.SetRefreshToken(refreshToken, user.Id);
            uow.Users.UpdateLoginTime(user.Id);
            return new Response(Message.SUCCESS, new TokenPresenter { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        public Response RefreshToken(string accessToken, string refreshToken)
        {
            var key = Encoding.ASCII.GetBytes(JwtUtil.SECRET_KEY);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);
            var userCredentialString = jwtToken.Claims.First(x => x.Type == "id").Value;
            int userId = Int32.Parse(userCredentialString);
            UserSchema user = uow.Users.FindOne(userId); 
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
