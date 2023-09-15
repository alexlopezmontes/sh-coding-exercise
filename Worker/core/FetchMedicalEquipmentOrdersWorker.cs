using com.synapse.order.domain.Interfaces;

namespace com.synapse.order.worker.core
{
    public class ProcessMedicalEquipmentDeliveredOrdersWorker : BackgroundService
    {
        private readonly ILogger<ProcessMedicalEquipmentDeliveredOrdersWorker> logger;
        private readonly IOrderService orderService;

        public ProcessMedicalEquipmentDeliveredOrdersWorker(
                      ILogger<ProcessMedicalEquipmentDeliveredOrdersWorker> logger,
					  IOrderService orderService
			)
        {
           this.logger = logger;
           this.orderService = orderService;
                
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("ProcessMedicalEquipmentDeliveredOrdersWorker running at: {time}", DateTimeOffset.Now);
                await orderService.ProcessMedicalEquipmentDeliveredOrders();
			}
        }
    }
}