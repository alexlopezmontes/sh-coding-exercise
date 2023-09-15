using com.synapse.order.dataaccess.Interfaces;
using com.synapse.order.domain.utility.DataTransferObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;

namespace com.synapse.order.dataaccess.Gateways
{
	public class OrderApiGateway : IOrderApiGateway
	{
		private readonly HttpClient httpClient;
		private readonly ILogger<OrderApiGateway> logger;

		public OrderApiGateway(HttpClient httpClient,
							   ILogger<OrderApiGateway> logger)
		{
			this.httpClient = httpClient;
			this.logger = logger;
		}
		
		public async Task<IList<Order>> GetOrdersByStatusAsync()
		{
			var response = await httpClient.GetAsync($"/orders");
			if (response.IsSuccessStatusCode)
			{
				if (response.Content != null)
				{
					var orders = await response.Content.ReadFromJsonAsync<Order[]>();
					if (orders != null) {
						return orders;
					}
				}
			}
			else
			{
				Console.WriteLine("Failed to fetch orders from API.");
			}
			return Array.Empty<Order>();

		}

		public async Task<bool> UpdateOrderAsync(Order order)
		{
			var response = await httpClient.PostAsJsonAsync("/orders", order);
			if (response.IsSuccessStatusCode)
			{
				logger.LogInformation($"Updated order sent for processing: OrderId {order.OrderId}");
			}
			else
			{
				logger.LogInformation($"Failed to send updated order for processing: OrderId {order.OrderId}");
			}
			return response.IsSuccessStatusCode;
		}
	}
}
