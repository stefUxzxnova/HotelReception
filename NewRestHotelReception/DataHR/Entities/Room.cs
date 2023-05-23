using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHR.Entities
{
	public class Room : BaseEntity
	{
		public int RoomNumber { get; set; }

		[StringLength(50)]
		public string RoomType { get; set; }

		[StringLength(350)]
		public string RoomDescription { get; set; }
		public decimal PricePerDay { get; set; }
		public int MaxOccupancy { get; set; }
		public int Floor { get; set; }
		public bool isOccupied { get; set; }
		public string ImageLink { get; set; }
	}
}
