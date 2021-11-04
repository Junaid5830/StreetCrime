using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
  public class SaveCardDTO
  {
    public string CardNumber { get; set; }
    public string NameOnCard { get; set; }
    public DateTime CardExpiry { get; set; }
    public string CVVNumber { get; set; }
  }

  public partial class CardDTO : SaveCardDTO
  {
    public int CardId { get; set; }
    public string UserId { get; set; }
  }
}
