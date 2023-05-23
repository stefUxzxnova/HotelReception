using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.Room
{
	public class EditVM
	{
		#region Properties
		public int Id { get; set; }
		
		[Display(Name = "Room number")]
		[Required (ErrorMessage = "Enter correct data!")]
		public int RoomNumber { get; set; }
		
		[Display(Name = "Room type")]
		[Required(ErrorMessage = "This field is Required!")]
		public string RoomType { get; set; }

		[Required(ErrorMessage = "This field is Required!")]
		[Display(Name = "Room description")]
		public string RoomDescription { get; set; }

		[Required(ErrorMessage = "This field is Required!")]
		[Display(Name = "Price per day")]
		public decimal PricePerDay { get; set; }
		[Required(ErrorMessage = "This field is Required!")]
		[Display(Name = "Max Occupancy")]
		public int MaxOccupancy { get; set; }

		[Required(ErrorMessage = "This field is Required!")]
		[Display(Name = "Floor")]
		public int Floor { get; set; }

		[Display(Name = "Image")]
		public string ImageLink { get; set; }
		#endregion
	}
}
