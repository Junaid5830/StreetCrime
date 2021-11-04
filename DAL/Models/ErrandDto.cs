using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
  public class CreateErrandDto
  {
    [Required]
    [StringLength(maximumLength: 50, ErrorMessage = "Value is too long")]
    public string Value { get; set; }
    [Required]
    [StringLength(maximumLength: 50, ErrorMessage = "Information is too long")]
    public string Information { get; set; }
  }

  public class UpdateErrandDto : CreateErrandDto
  {

  }

  public class ErrandDto : CreateErrandDto
  {
    public int Id { get; set; }
  }
}
