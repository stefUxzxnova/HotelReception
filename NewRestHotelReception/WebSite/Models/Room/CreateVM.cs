using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.Room
{
	public class CreateVM
	{
		#region Properties
		[Required]
		[Display(Name = "Room number")]
		public int RoomNumber { get; set; }

		[Required]
		[Display(Name = "Room type")]
		public string RoomType { get; set; }

		[Required]
		[Display(Name = "Room description")]
		public string RoomDescription { get; set; }

		[Required]
		[Display(Name = "Price per day")]
		public decimal PricePerDay { get; set; }

		[Required]
		[Display(Name = "Max Occupancy")]
		public int MaxOccupancy { get; set; }

		[Required]
		[Display(Name = "Floor")]
		public int Floor { get; set; }

		[Display(Name = "Image")]
		public string ImageLink { get; set; }
		#endregion
	}
}
