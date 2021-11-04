using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DAL.Data
{
  public class Category
  {
    [Key]
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public string IconPath { get; set; }
    public bool IsDeleted { get; set; }
  }
}
