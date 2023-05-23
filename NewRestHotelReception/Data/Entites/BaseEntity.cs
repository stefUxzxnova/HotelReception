using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entites
{
	public class BaseEntity
	{
		public int ID { get; set; }
		public DateTime? CreatedOn { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
