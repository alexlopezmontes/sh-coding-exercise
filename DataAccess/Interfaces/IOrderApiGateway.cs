using com.synapse.order.domain.utility.DataTransferObjects;
using com.synapse.order.domain.utility.Constants;

namespace com.synapse.order.dataaccess.Interfaces
{
	public interface IOrderApiGateway
	{
		Task<IList<Order>> GetOrdersByStatusAsync();
		Task<bool> UpdateOrderAsync(Order order);
	}
}
