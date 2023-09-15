using com.synapse.order.dataaccess.Gateways;
using com.synapse.order.dataaccess.Interfaces;
using com.synapse.order.domain.Services;
using com.synapse.order.domain.utility.DataTransferObjects;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests
{
	public class OrderServiceUnitTests
	{
		[Fact]
		public async void ProcessMedicalEquipmentDeliveredOrders_WhenNoOrderItemsAreDelivered_NoGatewayMethodIsCalled()
		{
			//Arrange
			var _httpClient = MockFixtureSetup.SetupOrderApiHttpClient(UseCaseDataEnum.OrdersWithoutItemsInDeliveredStatus);
			var _orderApiGateway = new OrderApiGateway(_httpClient, new Mock<ILogger<OrderApiGateway>>().Object);
			var _businessCommunicationsApiGateway = new Mock<IBusinessCommunicationApiGateway>();
			var _orderService = new OrderService(new Mock<ILogger<OrderService>>().Object, _orderApiGateway, _businessCommunicationsApiGateway.Object);

			//Act 
			await _orderService.ProcessMedicalEquipmentDeliveredOrders();

			//Assert
			_businessCommunicationsApiGateway.Verify(v => v.SendAlertMessageAsync(It.IsAny<string>(), It.IsAny<OrderItem>()), Times.Never);
		}

		[Fact]
		public async void ProcessMedicalEquipmentDeliveredOrders_WhenOrderItemsAreDelivered_NotificationIsSent()
		{
			//Arrange
			var _httpClient = MockFixtureSetup.SetupOrderApiHttpClient(UseCaseDataEnum.OrdersWithItemsInDeliveredStatus);
			var _orderApiGateway = new OrderApiGateway(_httpClient, new Mock<ILogger<OrderApiGateway>>().Object);
			var _businessCommunicationsApiGateway = new Mock<IBusinessCommunicationApiGateway>();
			var _orderService = new OrderService(new Mock<ILogger<OrderService>>().Object, _orderApiGateway, _businessCommunicationsApiGateway.Object);

			_businessCommunicationsApiGateway.Setup(s => s.SendAlertMessageAsync(It.IsAny<string>(), It.IsAny<OrderItem>())).Returns(Task.FromResult(true));
			//Act 
			await _orderService.ProcessMedicalEquipmentDeliveredOrders();

			//Assert
			// I didn't setup the SendAlert method to return true as my test only covers the method call
			_businessCommunicationsApiGateway.Verify(v => v.SendAlertMessageAsync(It.IsAny<string>(), It.IsAny<OrderItem>()), Times.Exactly(2));
		}
	}

}