using ApplicationService.DTOs;
using System.ComponentModel.DataAnnotations;
using WebSite.Models.Client;
using WebSite.Models.Room;

namespace WebSite.Models.Booking
{
    public class BookingVM
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "This field is required.")]
		public DateTime? CheckInDate { get; set; }
		[Required(ErrorMessage = "This field is required.")]
		public DateTime? CheckOutDate { get; set; }
        public decimal TotalCost { get; set; }
		[Required(ErrorMessage = "This field is required.")]
		public string PaymentStatus { get; set; }
        public decimal PricePerDay { get; set; }

        public int ClientID { get; set; }

        public virtual ClientVM ClientVM { get; set; }

        public int RoomID { get; set; }
        public virtual RoomVM RoomVM { get; set; }

        public List<RoomVM> roomVMs { get; set; }

        #region Contructors
        public BookingVM() { }

        public BookingVM(BookingDTO bookingDTO)
        {
            Id = bookingDTO.ID;
            CheckInDate = bookingDTO.CheckInDate;
            CheckOutDate = bookingDTO.CheckOutDate;
            TotalCost = bookingDTO.TotalCost;
            PaymentStatus = bookingDTO.PaymentStatus;
            ClientVM = new ClientVM
            {
                ClientID = bookingDTO.Client.ClientID,
                FirstName = bookingDTO.Client.FirstName,
                LastName = bookingDTO.Client.LastName,
            };
            RoomVM = new RoomVM
            {
                ID = bookingDTO.Room.ID,
                PricePerDay = bookingDTO.Room.PricePerDay,
                RoomNumber = bookingDTO.Room.RoomNumber,
            };
        }
		#endregion
		
	}
}
