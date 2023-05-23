using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
	public class RoomDTO : BaseDTO, IValidate
	{
		public int RoomNumber { get; set; }
	
		public string RoomType { get; set; }
		public string RoomDescription { get; set; }
		public decimal PricePerDay { get; set; }
		public int MaxOccupancy { get; set; }
		public int Floor { get; set; }
		public bool isOccupied { get; set; }
		public string ImageLink { get; set; }

		public bool Validate()
		{
			if (String.IsNullOrEmpty(RoomDescription))
			{
				return false;
			}
			if (RoomNumber <= 0 || PricePerDay <= 0 || MaxOccupancy <= 0 || Floor <= 0)
			{
				return false;
			}
			return true;
		}

		
	}
}
