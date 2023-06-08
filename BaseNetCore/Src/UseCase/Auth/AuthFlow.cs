
using BaseNetCore.Src.Utils;
using BaseNetCore.Src.Helper;
using BaseNetCore.Src.Helper.Constant;
using BaseNetCore.Src.Services;
using BaseNetCore.Src.Services.Schemas;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BaseNetCore.Src.UseCase.Auth
{
  public class AuthFlow
  {
    readonly IUser userService;
    readonly IAuth authService;
    public AuthFlow(IUser _userService, IAuth _authService)
    {
      userService = _userService;
      authService = _authService;
    }

    public Response Login(string username, string password)
    {
      Response response = userService.GetUser(username);
      if (response.Status == Message.ERROR)
      {
        return response;
      }
      User user = (User)response.Result;
      bool isMatched = Jwt.Compare(user.Password, password);
      if (!isMatched)
      {
        return new Response("error", "");
      }
      string accessToken = Jwt.GenerateAccessToken(user.Id);
      string refreshToken = Jwt.GenerateRefreshToken();
      authService.SetRefreshToken(refreshToken, user.Id);
      authService.UpdateLoginTime(user.Id);

      return new Response(Message.SUCCESS, new TokenPresenter { AccessToken = accessToken, RefreshToken = refreshToken });
    }

    public Response RefreshToken(string accessToken, string refreshToken)
    {
      var key = Encoding.ASCII.GetBytes(Jwt.SECRET_KEY);
      var tokenHandler = new JwtSecurityTokenHandler();
      var jwtToken = tokenHandler.ReadJwtToken(accessToken);
      var userCredentialString = jwtToken.Claims.First(x => x.Type == "id").Value;
      int userId = Int32.Parse(userCredentialString);
      Response response = userService.Get(userId);
      User user = (User)response.Result;
      bool isMatched = user.HashRefreshToken.Equals(refreshToken);
      if (isMatched)
      {
        var newToken = Jwt.GenerateAccessToken(userId);
        var newRefreshToken = Jwt.GenerateRefreshToken();

        return new Response(Message.SUCCESS, new
        {
          AccessToken = newToken,
          RefreshToken = newRefreshToken
        });
      }
      return new Response(Message.ERROR, new { });
    }

  }
}
