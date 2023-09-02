﻿using Base.Core.Schemas;
using Base.Utils;
using Base.Services.Base;
using Base.Services;

namespace Base.Application.UseCases
{

    public class GetCurrentUserFlow
    { 
        private readonly IUnitOfWork uow;
        public GetCurrentUserFlow(IUnitOfWork _uow)
        {
            uow = _uow; 
        }

        public Response GetCurrentUser(string token)
        {
            string userCredentialString = JwtUtil.GetIdByToken(token);
            int id = 0;
            if (!Int32.TryParse(userCredentialString, out id))
            {
                return new Response(Message.ERROR, null);
            } 
            UserSchema user = uow.Users.FindOne(id);
            return new Response(Message.SUCCESS, user);
        }
    }
}
