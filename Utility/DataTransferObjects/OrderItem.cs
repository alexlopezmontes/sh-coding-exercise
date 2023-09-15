
namespace com.synapse.order.domain.utility.DataTransferObjects
{
	public class OrderItem
	{
		public int Id { get; set; } = default!;
		public string Sku { get; set; } = default!;
		public string Status { get; set; } = default!;
		public string Description { get; set; } = default!;
		public int DeliveryNotification { get; set; } = default!;
		
	}
}
