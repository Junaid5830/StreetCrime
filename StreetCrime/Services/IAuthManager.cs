using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreetCrime.Services
{
  public interface IAuthManager
  {
    Task<bool> ValidateUser(LoginUserDTO userDTO);
    Task<string> CreateToken(string Email = null);
  }
}
