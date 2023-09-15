using com.synapse.order.dataaccess.Gateways;
using com.synapse.order.dataaccess.Interfaces;
using com.synapse.order.domain.Interfaces;
using com.synapse.order.domain.Services;
using com.synapse.order.domain.utility.Settings;
using com.synapse.order.worker.core;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices((hostContext, services) =>
	{
		services.AddHostedService<ProcessMedicalEquipmentDeliveredOrdersWorker>();
		services.AddSingleton<IOrderService, OrderService>();

		// http clients section
		var apiBaseAddresses = hostContext.Configuration.GetSection("Gateways").Get<GatewaySettings>();
		services.AddHttpClient<IOrderApiGateway,OrderApiGateway>(httpClient =>
		{
			httpClient.BaseAddress = new Uri(apiBaseAddresses.OrdersApiBaseUrl);
		});

		services.AddHttpClient<IBusinessCommunicationApiGateway, BusinessCommunicationApiGateway>(httpClient =>
		{
			httpClient.BaseAddress = new Uri(apiBaseAddresses.BusinessCommunicationsApiBaseUrl);
		});

	}).UseSerilog((hostingContext, loggerConfiguration) => 
	
	   loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
	 )  
	.Build();

await host.RunAsync();
