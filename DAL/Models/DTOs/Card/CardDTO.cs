using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
  public class CategoryDTO
  {
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public string IconPath { get; set; }
    public bool IsDeleted { get; set; }
  }
}
