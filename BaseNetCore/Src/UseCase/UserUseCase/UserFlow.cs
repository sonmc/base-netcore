using BaseNetCore.Src.Infrastructure.Helper;
using BaseNetCore.Src.Infrastructure.Helper.Constant;
using BaseNetCore.Src.Infrastructure.Schemas;
using BaseNetCore.Src.Services;
using BaseNetCore.Src.Utils;

namespace BaseNetCore.Src.UseCase.UserUseCase
{

  public class UserFlow
  {
    readonly IUserService userService;
    public UserFlow(IUserService _service)
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
