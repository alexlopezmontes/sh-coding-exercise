using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.synapse.order.domain.Interfaces
{
	public interface IOrderService
	{
		Task ProcessMedicalEquipmentDeliveredOrders();
	}
}
