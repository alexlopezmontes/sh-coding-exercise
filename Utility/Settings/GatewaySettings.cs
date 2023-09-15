using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.synapse.order.domain.utility.Settings
{
	public class GatewaySettings
	{
		public string OrdersApiBaseUrl { get; set; } = default!;
		public  string BusinessCommunicationsApiBaseUrl { get; set; } = default!;
	}
}
