using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
	public class UserDTO : BaseDTO, IValidate
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }

		public bool Validate()
		{
			if (String.IsNullOrEmpty(FirstName) || String.IsNullOrEmpty(LastName)
				|| String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
			{
				return false;
			}
			return true;
		}
	}
}
