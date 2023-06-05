﻿
namespace BaseNetCore.Src.Infrastructure.Schemas
{
  public class User : BaseSchema
  {
    public string UserName { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }
    public DateTime? LastLogin { get; set; }
    public string? HashRefreshToken { get; set; }
  }

}
