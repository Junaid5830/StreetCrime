﻿using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StreetCrime.Services
{
  public class AuthManager : IAuthManager
  {
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private User _user;

    public AuthManager(UserManager<User> userManager,
      IConfiguration configuration)
    {
      _userManager = userManager;
      _configuration = configuration;
    }
    public async Task<string> CreateToken(string Email = null)
    {
      if (Email != null)
        _user = await _userManager.FindByEmailAsync(Email);
      var signinCredentials = GetSigninCredentials();
      var claims = await GetClaims();
      var tokenOptions = GenerateTokenOptions(signinCredentials, claims);

      return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials, List<Claim> claims)
    {
      var jwtSettings = _configuration.GetSection("Jwt");
      var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(
        jwtSettings.GetSection("lifetime").Value));

      var token = new JwtSecurityToken(
        issuer: jwtSettings.GetSection("Issuer").Value,
        claims: claims,
        expires: expiration,
        signingCredentials: signinCredentials
        );

      return token;
    }

    private async Task<List<Claim>> GetClaims()
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name, _user.UserName)
      };

      var roles = await _userManager.GetRolesAsync(_user);

      foreach (var role in roles)
      {
        claims.Add(new Claim(ClaimTypes.Role, role));
      }

      return claims;
    }

    private SigningCredentials GetSigninCredentials()
    {
      var jwtSettings = _configuration.GetSection("Jwt");
      var key = jwtSettings.GetSection("Key").Value;
      var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

      return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public async Task<bool> ValidateUser(LoginUserDTO userDTO)
    {
      _user = await _userManager.FindByEmailAsync(userDTO.Email);

      return (_user != null && await _userManager.CheckPasswordAsync(_user, userDTO.Password));
    }
  }
}
