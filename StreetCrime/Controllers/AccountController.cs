using AutoMapper;
using StreetCrime.Services;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StreetCrime.Configurations;
using Common;
using DAL.IRepository;

namespace StreetCrime.Controllers
{
  public class AccountController : BaseController
  {
    #region Global
    private readonly UserManager<User> _userManager;
    private readonly IAuthManager _authManager;

    public AccountController(UserManager<User> userManager,
      ILogger<AccountController> logger,
      IAuthManager authManager, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper, logger)
    {
      _userManager = userManager;
      _authManager = authManager;
    }
    #endregion

    #region User

    #region Register User
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO userDTO)
    {
      _logger.LogInformation($"Registeration Attempt for {userDTO.Email}");
      if (!ModelState.IsValid)
      {
        return BadRequest(_customResponses.SendErrorResponse(ModelState));
      }

      try
      {
        var user = _mapper.Map<User>(userDTO);

        user.UserName = userDTO.Email;
        user.CreatedOn = DateTime.Now;
        user.IsActive = true;


				var result = await _userManager.CreateAsync(user, userDTO.Password);

        if (!result.Succeeded)
        {
          foreach (var error in result.Errors)
          {
            ModelState.AddModelError(error.Code, error.Description);
          }
          return BadRequest(_customResponses.SendErrorResponse(ModelState));
        }

        IList<string> roles = new List<string>(new string[] { "User" });

        await _userManager.AddToRolesAsync(user, roles);

        var returnUserDTO = _mapper.Map<UserDTO>(user);
        returnUserDTO.Password = userDTO.Password;

        return Ok(_customResponses.SendSuccessResponse(new { Token = await _authManager.CreateToken(userDTO.Email), User = returnUserDTO }));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Something went wrong in the {nameof(RegisterUser)}");
        return StatusCode(500, _customResponses.SendErrorResponse($"Something Went Wrong in the {nameof(RegisterUser)}"));
      }
    }
    #endregion

    #region LoginUser
    [HttpPost]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO loginDTO)
    {
      _logger.LogInformation($"LoginUser Attempt for {loginDTO.Email}");
      if (!ModelState.IsValid)
      {
        return BadRequest(_customResponses.SendErrorResponse(ModelState));
      }

      try
      {
        if (!await _authManager.ValidateUser(loginDTO))
        {
          return Unauthorized(_customResponses.SendErrorResponse("Unauthorized access"));
        }

        var user = await _userManager.FindByEmailAsync(loginDTO.Email);
        var roles = await _userManager.GetRolesAsync(user);

        if (roles.FirstOrDefault() != "User")
        {
          ModelState.AddModelError("", ResponseMessages.UserAccountNotFound);
        }

        if (!ModelState.IsValid)
        {
          return BadRequest(_customResponses.SendErrorResponse(ModelState));
        }

        var newuser = await _unitOfWork.Users.Get(x => x.Id == user.Id, new List<string> { "Vehicles" });

        var userDTO = _mapper.Map<UserDTO>(newuser);
        userDTO.Password = loginDTO.Password;

        return Ok(_customResponses.SendSuccessResponse(new { Token = await _authManager.CreateToken(), User = userDTO }));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Something went wrong in the {nameof(LoginUser)}");
        return StatusCode(500, _customResponses.SendErrorResponse($"Something Went Wrong in the {nameof(LoginUser)}"));
      }
    }
    #endregion

    #endregion

		#region General

		#region SendOtpPhone
		[HttpPost]
    public IActionResult SendOtpPhone([FromBody] UserOtpPhoneDTO phoneOtp)
    {
      _logger.LogInformation($"OTP sending for {phoneOtp.PhoneNumber}");
      if (!ModelState.IsValid)
      {
        return BadRequest(_customResponses.SendErrorResponse(ModelState));
      }

      try
      {
        var result = _unitOfWork.Users.DoesPhoneNumberExist(phoneOtp.PhoneNumber);

        if (result)
        {
          ModelState.AddModelError("", ResponseMessages.PhoneNotUnique);
        }

        if (!ModelState.IsValid)
        {
          return BadRequest(_customResponses.SendErrorResponse(ModelState));
        }

        // TODO: implement sms services
        //var otp = Utilities.GenerateRandom();

        var otp = "111111";
        return Ok(_customResponses.SendSuccessResponse(new { OTP = otp }));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Something went wrong in the {nameof(SendOtpPhone)}");
        return StatusCode(500, _customResponses.SendErrorResponse($"Something Went Wrong in the {nameof(SendOtpPhone)}"));
      }
    }
    #endregion


    #endregion
  }
}
