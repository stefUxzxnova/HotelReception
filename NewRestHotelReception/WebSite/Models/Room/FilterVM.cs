using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.Room
{
	public class FilterVM
	{
        [RegularExpression("^[0-9]+$", ErrorMessage = "Room number must be numeric.")]
        public int RoomNumber { get; set; }
		public string RoomType { get; set;}
	}
}
