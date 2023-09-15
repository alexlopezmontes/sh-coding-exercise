using com.synapse.order.domain.utility.Constants;
using com.synapse.order.domain.utility.DataTransferObjects;
using Moq;
using Moq.Protected;
using System.Net;

namespace Tests
{
	internal class MockFixtureSetup
	{

		private static readonly Dictionary<UseCaseDataEnum, IList<Order>> contentByUseCase = new Dictionary<UseCaseDataEnum, IList<Order>> () {
			{UseCaseDataEnum.OrdersWithoutItemsInDeliveredStatus, new List<Order>() { 
			      new Order ()
				  {
					  OrderId = "O1",
					  OrderDate = DateTime.Now,
					  OrderItems = new List<OrderItem> ()
					  {
						 new OrderItem ()
						 {
							 Id = 1,
							 Description = "Crutch",
							 Status = OrderItemStatus.AwaitingPayment
						 },
						 new OrderItem ()
						 {
							 Id = 2,
							 Description = "Nebulizer",
							 Status = OrderItemStatus.AwaitingPayment
						 }
					  }
				  },
				  new Order ()
				  {
					  OrderId = "O2",
					  OrderDate = DateTime.Now,
					  OrderItems = new List<OrderItem> ()
					  {
						 new OrderItem ()
						 {
							 Id = 1,
							 Description = "Wheelchair",
							 Status = OrderItemStatus.Approved
						 },
						 new OrderItem ()
						 {
							 Id = 2,
							 Description = "Walker",
							 Status = OrderItemStatus.Approved
						 }
					  }
				  },

				}
			},
			{UseCaseDataEnum.OrdersWithItemsInDeliveredStatus, new List<Order>() {
				  new Order ()
				  {
					  OrderId = "O1",
					  OrderDate = DateTime.Now,
					  OrderItems = new List<OrderItem> ()
					  {
						 new OrderItem ()
						 {
							 Id = 1,
							 Description = "Crutch",
							 Status = OrderItemStatus.Delivered
						 },
						 new OrderItem ()
						 {
							 Id = 2,
							 Description = "Nebulizer",
							 Status = OrderItemStatus.Shipped
						 }
					  }
				  },
				  new Order ()
				  {
					  OrderId = "O2",
					  OrderDate = DateTime.Now,
					  OrderItems = new List<OrderItem> ()
					  {
						 new OrderItem ()
						 {
							 Id = 1,
							 Description = "Wheelchair",
							 Status = OrderItemStatus.Delivered
						 },
						 new OrderItem ()
						 {
							 Id = 2,
							 Description = "Walker",
							 Status = OrderItemStatus.Approved
						 }
					  }
				  },

				}
			},
	   };

	   public static HttpClient SetupOrderApiHttpClient(UseCaseDataEnum useCase)
		{
			var testContent = contentByUseCase[useCase];

			var mockMessageHandler = new Mock<HttpMessageHandler>();
			mockMessageHandler.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(new HttpResponseMessage
				{
					StatusCode = HttpStatusCode.OK,
					Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(testContent))
				});
			var httpClient = new HttpClient(mockMessageHandler.Object)
			{
				BaseAddress = new Uri("https://mock.com")
			};
			return httpClient;


		}
	}
}
