using BaseNetCore.Src.Helper;
using BaseNetCore.Src.Helper.Constant;
using BaseNetCore.Src.Services;
using BaseNetCore.Src.Services.Schemas;
using BaseNetCore.Src.Utils;

namespace BaseNetCore.Src.UseCase.User
{

  public class UserFlow
  {
    readonly IUser userService;
    public UserFlow(IUser _service)
    {
      userService = _service;
    }

    public Response GetCurrentUser(string token)
    {
      string userCredentialString = Jwt.GetIdByToken(token);
      int id = 0;
      if (!Int32.TryParse(userCredentialString, out id))
      {
        return new Response(Message.ERROR, null);
      }
      Response response = userService.Get(id);

      if (response.Status == Message.ERROR)
      {
        return new Response(Message.ERROR, null);
      }
      User user = (User)response.Result;
      return new Response(Message.SUCCESS, user);


    }
  }
}
