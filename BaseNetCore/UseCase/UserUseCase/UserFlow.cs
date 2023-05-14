using BaseNetCore.Infrastructure.Helper;
using BaseNetCore.Infrastructure.Helper.Constant;
using BaseNetCore.Infrastructure.Schemas;
using BaseNetCore.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BaseNetCore.UseCase.UserUseCase
{
    public interface IUserFlow
    {
        Response GetCurrentUser(string token);
    }

    public class UserFlow : IUserFlow
    {
        readonly IUserService userService;
        private readonly AppSettings appSettings;
        public UserFlow(IUserService _service, IOptions<AppSettings> _appSettings)
        {
            userService = _service;
            appSettings = _appSettings.Value;
        }

        public Response GetCurrentUser(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); 
            var key = Encoding.ASCII.GetBytes(appSettings.Secret); 
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userCredentialString = jwtToken.Claims.First(x => x.Type == "id").Value;
            int id = 0;
            if (!Int32.TryParse(userCredentialString, out id))
            {
                return new Response(Message.ERROR, null);
            } 
            Response response =  userService.Get(id);

            if (response.Status == Message.ERROR)
            {
                return new Response(Message.ERROR, null);
            }
            User user = (User)response.Result; 
            return new Response(Message.SUCCESS, user);
            
        }
    }
}
