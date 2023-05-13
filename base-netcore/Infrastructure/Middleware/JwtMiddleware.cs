using base_netcore.Business.Rule;
using base_netcore.Infrastructure.Helper;
using base_netcore.Infrastructure.Repositories;
using base_netcore.Infrastructure.Schemas;
using base_netcore.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace base_netcore.Infrastructure.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token != null)
                {
                    AttachUserToContext(context, userService, token);
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
            }
        }

        private void AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
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

            User user = null;
            Response response = userService.Get(id);
            user = (User)response.Result;
            if (user != null)
            {
                context.Items["User"] = user;
                bool isAdmin = user.Roles.Any(u => u.Id == Constant.ADMIN_ID);
                if (!isAdmin)
                {
                    CheckPermission(context, user, userService);
                }
            }
            else
            {
                context.Items["User"] = null;
            }
        }

        private void CheckPermission(HttpContext context, User user, IUserService userService)
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

                //bool isAccess = userService.CheckPermissionAction(user, action);
                //if (!isAccess)
                //{
                //    throw new ForbiddenException();
                //}
            }
        }
    }
}
