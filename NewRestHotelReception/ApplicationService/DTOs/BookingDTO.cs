using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
	public class BookingDTO : BaseDTO
	{
		public DateTime? CheckInDate { get; set; }
		public DateTime? CheckOutDate { get; set; }
		public decimal TotalCost { get; set; }
		public string PaymentStatus { get; set; }
		public decimal PricePerDay { get; set; }

	
		public int ClientID { get; set; }
		public virtual ClientDTO Client { get; set; }

		public int RoomID { get; set; }
		public virtual RoomDTO Room { get; set; }

		//public bool Validate()
		//{
		//	if (Client.ClientID <= 0)
		//	{
		//		return false;
		//	}
		//	return true;
		//}
	}
}
