using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
	public class OrderHeader
	{
		[Key]
		public int Id { get; set; }
		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }
		public DateTime OrderDate { get; set; } 
		public double OrderTotal { get; set; }	
		public string OrderStatus { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string District { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string PostCode { get; set; }



	}
}
