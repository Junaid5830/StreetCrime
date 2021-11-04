using System;
using System.ComponentModel.DataAnnotations;


namespace DAL.Models
{
	public class SaveVehicleDTO
	{
		//[Required]
		public string Make { get; set; }
		//[Required]
		public string Model { get; set; }
		//[Required]
		public string LicensePlateNumber { get; set; }
		public string Picture { get; set; }
		//[Required]
		public int TransmissionTypeId { get; set; }
	}

	public partial class VehicleDTO : SaveVehicleDTO
	{
		public int VehicleId { get; set; }
		public string UserId { get; set; }
	}
}

