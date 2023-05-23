using System.ComponentModel.DataAnnotations;
using WebSite.Models.Room;

namespace WebSite.Models.Booking
{
    public class CreateBookingVM
    {
        public int ClientID { get; set; }
        public int RoomID { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

        [Display(Name = "Payment status")]
        [Required(ErrorMessage = "This field is required.")]
        public string PaymentStatus { get; set; }

		
	}
}
