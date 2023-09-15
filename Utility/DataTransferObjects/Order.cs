namespace com.synapse.order.domain.utility.DataTransferObjects
{
    public class Order
    {
        public string OrderId { get; set; } = default!;
		public DateTime OrderDate { get; set; }
        public IList<OrderItem> OrderItems { get; set; } = default!;
	}
}