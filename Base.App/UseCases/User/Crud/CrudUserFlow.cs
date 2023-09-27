﻿using Base.Core.Schemas;
using Base.Utils;
using Base.Services;
using Base.Business.Rule;

namespace Base.App.UseCases
{

    public class CrudUserFlow
    {
        private readonly IUnitOfWork uow;
        public CrudUserFlow(IUnitOfWork _uow)
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
            User user = uow.Users.FindOne(id);
            return new Response(Message.SUCCESS, user);
        }

        public Response List()
        {
            var users = uow.Users.FindAll();
            users = users.Where(u => u.Id != UserRule.ADMIN_ID).ToList();
            return new Response(Message.SUCCESS, users);
        }

        public async Task<Response> Create(User user)
        {
            user.Password = JwtUtil.MD5Hash(UserRule.DEFAULT_PASSWORD);
            var result = uow.Users.Create(user);
            return new Response(Message.SUCCESS, result);
        }

        public async Task<Response> Update(User user)
        {
            var result = uow.Users.Update(user);
            return new Response(Message.SUCCESS, result);
        }

        public async Task<Response> Deletes(int[] ids)
        {
            var result = uow.Users.Deletes(ids);
            return new Response(Message.SUCCESS, result);
        }

    }
}