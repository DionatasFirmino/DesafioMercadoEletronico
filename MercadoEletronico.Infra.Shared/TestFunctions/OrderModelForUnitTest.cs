using MercadoEletronico.Domain.Models;
using System.Collections.Generic;

namespace MercadoEletronico.Infra.Shared.TestFunctions
{
	public static class OrderModelForUnitTest
	{
		public static OrderModel GetOrderModel() => new OrderModel
		{
			OrderId = "123456",
			Itens = GetItemForOrderModel()
		};

		private static List<ItemOrderModel> GetItemForOrderModel()
		{
			var items = new List<ItemOrderModel>();

			items.Add(new ItemOrderModel
			{
				Description = "Item A",
				PriceUnit = 10,
				Qtd = 1
			});

			items.Add(new ItemOrderModel
			{
				Description = "Item B",
				PriceUnit = 5,
				Qtd = 2
			});

			return items;
		}
	}
}
