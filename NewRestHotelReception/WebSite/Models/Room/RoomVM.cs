using ApplicationService.DTOs;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.Room
{
	public class RoomVM
	{
        #region Properties
        public int ID { get; set; }
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
        [Required]
        [Display(Name = "Is room occupied?")]
        public bool isOccupied { get; set; }
		#endregion

		#region Contructors
		public RoomVM() { }

        public RoomVM(RoomDTO roomDTO)
        {
            ID = roomDTO.ID;
            RoomType = roomDTO.RoomType;
            RoomDescription = roomDTO.RoomDescription;
            RoomNumber = roomDTO.RoomNumber;
            Floor = roomDTO.Floor;
            isOccupied = roomDTO.isOccupied;
            MaxOccupancy = roomDTO.MaxOccupancy;
            PricePerDay = roomDTO.PricePerDay;
        }

        #endregion
    }
}
