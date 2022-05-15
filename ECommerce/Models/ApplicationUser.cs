using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
	public class ApplicationUser:IdentityUser
	{
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		public string Adress { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string PostCode { get; set; }
		[NotMapped]
		public string Role { get; set; }
	}
}
