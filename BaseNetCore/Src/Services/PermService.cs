﻿
using BaseNetCore.Src.Helper;
using BaseNetCore.Src.Services.Schemas;

namespace BaseNetCore.Src.Services
{
  public interface IPermService
  {
    Response Get();
  }

  public class PermService : IPermService
  {
    private readonly DataContext context;
    public PermService()
    {
      this.context = new DataContext();
    }

    public Response Get()
    {
      throw new NotImplementedException();
    }
  }
}
