
using BaseNetCore.Src.Utils;
using BaseNetCore.Src.Infrastructure.Helper;
using BaseNetCore.Src.Infrastructure.Helper.Constant;
using BaseNetCore.Src.Infrastructure.Schemas;

namespace BaseNetCore.Src.Services
{
  public interface IUserService
  {
    Response GetUser(string username);
    Response Get(int id);
    Response CheckPermission(User user, string actionName);
    Response Compare(string userPassword, string password);
    Response UpdateLoginTime(int userId);
    Response SetRefreshToken(string refreshToken, int userId);
  }

  public class UserService : IUserService
  {

    private readonly DataContext context;
    public UserService()
    {
      this.context = new DataContext();
    }

    public Response GetUser(string username)
    {
      User user = context.Users.Where(x => x.UserName.Equals(username)).FirstOrDefault();
      return new Response(user == null ? Message.ERROR : Message.SUCCESS, user);
    }

    public Response CheckPermission(User user, string actionName)
    {
      bool isAccess = false;
      //List<Permission> apis = permRepository.Get((int)user.RoleId);
      //foreach (var a in apis)
      //{
      //    if (a.Name.ToLower().IndexOf(actionName.ToLower()) > -1)
      //    {
      //        isAccess = true;
      //    }
      //}
      return new Response(Message.SUCCESS, isAccess);
    }

    public Response Get(int id)
    {
      User user = context.Users.Find(id);
      return new Response(user == null ? Message.ERROR : Message.SUCCESS, user);
    }

    public Response Compare(string userPassword, string password)
    {
      string hash = Jwt.MD5Hash(password);
      if (hash.ToLower().Equals(userPassword.ToLower()))
      {
        return new Response(Message.SUCCESS, hash);
      }
      return new Response(Message.ERROR, hash);
    }

    public Response UpdateLoginTime(int userId)
    {
      User user = context.Users.Find(userId);
      user.LastLogin = DateTime.UtcNow;
      return new Response(Message.SUCCESS, user);
    }

    public Response SetRefreshToken(string refreshToken, int userId)
    {
      User user = context.Users.Find(userId);
      user.HashRefreshToken = refreshToken;
      context.Users.Update(user);
      return new Response(Message.SUCCESS, user);
    }

  }
}
