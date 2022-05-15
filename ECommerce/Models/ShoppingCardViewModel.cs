using System.Collections.Generic;

namespace ECommerce.Models
{
	public class ShoppingCardViewModel
	{
		public IEnumerable<ShoppingCart> ListCart { get; set; }
		public OrderHeader OrderHeader { get; set; }	
	}
}
