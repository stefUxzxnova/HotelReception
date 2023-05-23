using ApplicationService.DTOs;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.Room
{
    public class DetailSVM
    {
        #region Properties
        public int ID { get; set; }

        [Display(Name = "Room number")]
        public int RoomNumber { get; set; }

        [Display(Name = "Room type")]
        public string RoomType { get; set; }

        [Display(Name = "Room description")]
        public string RoomDescription { get; set; }

        [Display(Name = "Price per day")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Max Occupancy")]
        public int MaxOccupancy { get; set; }

        [Display(Name = "Floor")]
        public int Floor { get; set; }

        [Display(Name = "Is room occupied?")]
        public bool isOccupied { get; set; }

        public string ImageLink { get; set; }

        #endregion

        #region Contructors
        public DetailSVM() { }

        public DetailSVM(RoomDTO roomDTO)
        {
            ID = roomDTO.ID;
            RoomType = roomDTO.RoomType;
            RoomDescription = roomDTO.RoomDescription;
            RoomNumber = roomDTO.RoomNumber;
            Floor = roomDTO.Floor;
            isOccupied = roomDTO.isOccupied;
            MaxOccupancy = roomDTO.MaxOccupancy;
            PricePerDay = roomDTO.PricePerDay;
            ImageLink = roomDTO.ImageLink;
        }

        #endregion
    }
}
