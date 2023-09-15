using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.synapse.order.domain.utility.Constants
{
	public static class OrderItemStatus
	{
		public static readonly string AwaitingPayment = "AwaitingPayment";
		public static readonly string Approved = "Approved";
		public static readonly string Shipped = "Shipped";
		public static readonly string Delivered = "Delivered";
	}
}
