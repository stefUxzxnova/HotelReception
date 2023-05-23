using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
	public class ClientDTO : IValidate
	{
        public int ClientID { get; set; }
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string City { get; set; }
		public string Email { get; set; }
		public bool Validate()
		{
			if (String.IsNullOrEmpty(FirstName) || String.IsNullOrEmpty(LastName)
				|| String.IsNullOrEmpty(Phone) || String.IsNullOrEmpty(City) || String.IsNullOrEmpty(Email))
			{
				return false;
			}
			return true;
		}
	}
}
