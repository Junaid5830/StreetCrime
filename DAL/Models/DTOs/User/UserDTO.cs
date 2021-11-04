using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
  public class UserOtpPhoneDTO
  {
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
  }
  public class LoginUserDTO 
  {
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [StringLength(15, ErrorMessage = "Your Password is limited to {2} to {1} characters", MinimumLength = 6)]
    public string Password { get; set; }
  }

  public class UserDetailDTO : LoginUserDTO
  {
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
  }

  public class RegisterUserDTO : UserDetailDTO
  {

  }

  public class UserDTO : UserDetailDTO
  {
    public string Id { get; set; }
    public DateTime CreatedOn { get; set; }
  }
}
