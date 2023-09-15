using com.synapse.order.dataaccess.Interfaces;
using com.synapse.order.domain.utility.DataTransferObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace com.synapse.order.dataaccess.Gateways
{
	public class BusinessCommunicationApiGateway : IBusinessCommunicationApiGateway
	{
		private readonly HttpClient httpClient;
		private readonly ILogger<BusinessCommunicationApiGateway> logger;
		public BusinessCommunicationApiGateway(HttpClient httpClient, ILogger<BusinessCommunicationApiGateway> logger)
		{
			this.logger = logger;
			this.httpClient = httpClient;
		}
		public async Task<bool> SendAlertMessageAsync(string orderId, OrderItem orderItem)
		{
			var alertData = new
			{
				Message = $"Alert for delivered item: Order {orderId}, Item: {orderItem.Description}, " +
						  $"Delivery Notifications: {orderItem.DeliveryNotification}"
			};
			var response = await httpClient.PostAsJsonAsync("/alerts", alertData);

			if (response.IsSuccessStatusCode)
			{
				logger.LogInformation($"Alert sent for delivered item: {orderItem.Description}");
			}
			else
			{
				logger.LogInformation($"Failed to send alert for delivered item: {orderItem.Description}");
			}
			return response.IsSuccessStatusCode;
		}
	}
}
