using MercadoEletronico.Domain.Entities;
using MercadoEletronico.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace MercadoEletronico.Infra.Shared.Mapper
{
	public static class OrderMapper
	{
		public static Order ConvertToOrderEntitie(this OrderModel orderModel) =>
			orderModel == null ? null : new Order
			{
				OrderId = orderModel.OrderId
			};

		public static OrderModel ConvertToOrderModel(this Order order) =>
			order == null ? null : new OrderModel
			{
				OrderId = order.OrderId
			};

		public static IEnumerable<OrderModel> ConvertToListOrderModel(this IEnumerable<Order> orders) =>
			new List<OrderModel>(orders.Select(o => o.ConvertToOrderModel()));
	}
}
