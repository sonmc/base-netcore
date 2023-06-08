
using BaseNetCore.Src.Utils;
using BaseNetCore.Src.Helper;
using BaseNetCore.Src.Helper.Constant;
using BaseNetCore.Src.Services.Schemas;

namespace BaseNetCore.Src.Services
{
  public interface IUser
  {
    Response GetUser(string username);
    Response Get(int id);
    Response CheckPermission(User user, string actionName);

  }

  public class UserService : IUser
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
