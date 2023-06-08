using BaseNetCore.Src.Helper;
using BaseNetCore.Src.Helper.Constant;
using BaseNetCore.Src.Services.Schemas;

namespace BaseNetCore.Src.Services
{
  public interface IAuth
  {
    Response UpdateLoginTime(int userId);
    Response SetRefreshToken(string refreshToken, int userId);

  }

  public class AuthService : IAuth
  {

    private readonly DataContext context;
    public AuthService()
    {
      this.context = new DataContext();
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
