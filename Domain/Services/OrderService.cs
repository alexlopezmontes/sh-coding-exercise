using com.synapse.order.dataaccess.Gateways;
using com.synapse.order.dataaccess.Interfaces;
using com.synapse.order.domain.Interfaces;
using com.synapse.order.domain.utility.DataTransferObjects;
using com.synapse.order.domain.utility.Constants;
using Microsoft.Extensions.Logging;


namespace com.synapse.order.domain.Services
{
	public class OrderService: IOrderService
	{
		private readonly ILogger<OrderService> logger;
		private readonly IOrderApiGateway orderApiGateway;
		private readonly IBusinessCommunicationApiGateway businessCommunicationApiGateway;

		public OrderService(ILogger<OrderService> logger, IOrderApiGateway orderApiGateway,
			IBusinessCommunicationApiGateway businessCommunicationApiGateway) {
			this.logger = logger;
			this.orderApiGateway = orderApiGateway;	
			this.businessCommunicationApiGateway = businessCommunicationApiGateway;
		}

		public async Task ProcessMedicalEquipmentDeliveredOrders()
		{
			var orders = await orderApiGateway.GetOrdersByStatusAsync();

			var ordersToProcess = FilterOrdersWithDeliveredItemsToProcess(orders);
			logger.LogInformation($"{ordersToProcess.Count()} orders have items marked as delivered");
			foreach (var order in ordersToProcess)
			{
				var deliveredItems = order.OrderItems.Where(oi => oi.Status.Equals(OrderItemStatus.Delivered, StringComparison.OrdinalIgnoreCase));
                
				foreach (var orderItem in deliveredItems)
				{
					//TODO replace the two statements below with an ItemDeliveredEvent being raised
					//     and have these two functions as subscribers.

					// I didn't do it because of:
					//   a) simplicity and keeping an eye on submiting the assestment on time.
					//   b) The document says that we need the alert to be send before updating the order.
					//      I think these two should not be secuential/dependent.
					if (await businessCommunicationApiGateway.SendAlertMessageAsync(order.OrderId, orderItem))
					{
						orderItem.DeliveryNotification++;
						await orderApiGateway.UpdateOrderAsync(order);
					}
				}
			}
		}

		//TODO this could potentially be moved to the Order domain object and have it unit tested.
		private static IEnumerable<Order> FilterOrdersWithDeliveredItemsToProcess (IList<Order> orders)
		{
			if ( orders== null)
			{
				throw new ArgumentNullException(nameof(orders));
			}
			return orders.Where(o => o.OrderItems.Any( oi => oi.Status.Equals(OrderItemStatus.Delivered, StringComparison.OrdinalIgnoreCase)
													 // && oi.DeliveryNotification == default

													 // I didn't see the value of the DeliveryNotification property
													 // with the information I have but we could use it to suppress 
													 // order items from being notified more than once 
													 )
							   );
		}
	}
}
