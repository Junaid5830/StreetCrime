using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Data
{
  public class User : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedOn { get; set; }
  }
}
