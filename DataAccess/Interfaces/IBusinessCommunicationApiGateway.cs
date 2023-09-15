using com.synapse.order.domain.utility.DataTransferObjects;

namespace com.synapse.order.dataaccess.Interfaces
{
	public interface IBusinessCommunicationApiGateway
	{
		Task<bool> SendAlertMessageAsync(string orderId, OrderItem orderItem);
	}
}
