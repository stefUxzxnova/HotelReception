using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entites
{
	public class Booking : BaseEntity
	{
		public DateTime? CheckInDate { get; set; }
		public DateTime? CheckOutDate { get; set; }
		public decimal? PartialPayment { get; set; }
		public decimal TotalCost { get; set; }
		public string PaymentStatus { get; set; }

		public int ClientID { get; set; }
		public virtual Client Client { get; set; }
		public int RoomID { get; set; }
		public virtual Room Room { get; set; }
	}
}
