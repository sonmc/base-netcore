﻿
using Base.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Base.Utils;
using Base.Core.Schemas;

namespace BaseNetCore.Src.UseCase.Auth
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
            Response response = uow.User.Get(username);
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
            uow.User.SetRefreshToken(refreshToken, user.Id);
            uow.User.UpdateLoginTime(user.Id);
            return new Response(Message.SUCCESS, new TokenPresenter { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        public Response RefreshToken(string accessToken, string refreshToken)
        {
            var key = Encoding.ASCII.GetBytes(JwtUtil.SECRET_KEY);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);
            var userCredentialString = jwtToken.Claims.First(x => x.Type == "id").Value;
            int userId = Int32.Parse(userCredentialString);
            Response response = uow.User.Get(userId);
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
