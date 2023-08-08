using Base.Business.Rule;
using Base.Core.Exception;
using Base.Core.Schemas;
using Base.Services;
using Base.Utils; 
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Base.Application.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next; 

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next; 
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                //if (token != null)
                //{
                //    AttachUserToContext(context, uow, token);
                //}
                await _next(context);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void AttachUserToContext(HttpContext context, IUnitOfWork uow, string token)
        {
            if (context.Request.Path.Value != null && context.Request.Path.Value.ToLower() == "/api/auth/refreshtoken")
            {
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtUtil.SECRET_KEY);
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userCredentialString = jwtToken.Claims.First(x => x.Type == "id").Value;
            int id = Int32.Parse(userCredentialString);
            UserSchema user = uow.Users.Get(id);
            if (user != null)
            {
                context.Items["User"] = user;
                if (user.Id != UserRule.ADMIN_ID)
                {
                    CheckPermission(context, user.Id, uow.Users);
                } 
            }
        }

        private void CheckPermission(HttpContext context, int userId, IUser userService)
        {
            var header = context.Request.Path.Value;
            bool isRequestApi = header.Contains("/api/");
            if (isRequestApi)
            {
                var apiRequest = header.Replace("/api/", "").Split("/");
                string action = "";
                if (apiRequest.Length > 1)
                {
                    var isNumeric = apiRequest.Length == 2 ? int.TryParse(apiRequest[1], out int n) : false;
                    action = isNumeric ? apiRequest[0] : apiRequest[0] + "/" + apiRequest[1];
                }
                else
                {
                    action = apiRequest[0];
                }

                bool isAccess = userService.CheckPermissionAction(userId, action);
                if (!isAccess)
                {
                    throw new ForbiddenException();
                }
            }
        }
    }
}
