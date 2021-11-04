using AutoMapper;
using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreetCrime.Configurations
{
  public class MapperInitializer: Profile
  {
    public MapperInitializer()
    {
      CreateMap<User, RegisterUserDTO>().ReverseMap();
      CreateMap<User, UserDTO>().ReverseMap();
    }
  }
}
