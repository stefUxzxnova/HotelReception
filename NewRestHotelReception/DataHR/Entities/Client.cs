using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHR.Entities
{
	public class Client : BaseEntity
	{
		[StringLength(50)]
		public string FirstName { get; set; }

		[StringLength(50)]
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string City { get; set; }
		public string Email { get; set; }
		public bool IsCurrentlyInHotel { get; set; }
	}
}
