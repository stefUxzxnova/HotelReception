using WebSite.Models.Room;

namespace WebSite.Models.Booking
{
	public class CheckAvailabilityVM
	{
		public int ClientID { get; set; }
		public int RoomID { get; set; }
		public DateTime? CheckInDate { get; set; }
		public DateTime? CheckOutDate { get; set; }

		public List<RoomVM> roomVMs { get; set; }

		public bool IsValidBooking()
		{
			if (CheckInDate > CheckOutDate || CheckInDate < DateTime.Now)
			{
				return false;
			}
			return true;
		}
	}
}
