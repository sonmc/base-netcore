﻿using Base.App.Helpers;
using Base.Business.Rule;
using Base.Core.Exception;
using Base.Core.Schemas;
using Base.Services;
using Base.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Base.App.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork uow)
        {
            try
            {
                var token = context.Request.Cookies["access_token"];
                if (token != null)
                {
                    AttachUserToContext(context, uow, token);
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                var middlewareHelper = new MiddlewareHelper();
                await middlewareHelper.HandleExceptionAsync(context, ex);
            }
        }

        private void AttachUserToContext(HttpContext context, IUnitOfWork uow, string token)
        {
            if (context.Request.Path.Value != null && context.Request.Path.Value.ToLower() == "/api/auth/refresh-token")
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
            User user = uow.Users.FindOne(id);
            if (user != null)
            {
                context.Items["User"] = user;
                if (user.Id != UserRule.ADMIN_ID)
                {
                    CheckPermission(context, user.Id, uow);
                }
            }
        }

        private void CheckPermission(HttpContext context, int userId, IUnitOfWork uow)
        {
            var header = context.Request.Path.Value;
            bool isRequestApi = header.Contains("/api/");
            if (isRequestApi)
            {
                var apiRequest = header.Replace("/api/", "").Split("/");
                string action = "";
                string module = "";
                if (apiRequest.Length > 1)
                {
                    var isNumeric = apiRequest.Length == 2 ? int.TryParse(apiRequest[1], out int n) : false;
                    action = isNumeric ? apiRequest[0] : apiRequest[0] + "/" + apiRequest[1];
                    module = apiRequest[1];
                }
                else
                {
                    action = apiRequest[0];
                    module = apiRequest[0];
                }

                bool isAccess = uow.Users.CheckPermission(userId, module, action);
                if (!isAccess)
                {
                    throw new ForbiddenException();
                }
            }
        }
    }
}