using MercadoEletronico.Domain.Entities;
using System.Collections.Generic;

namespace MercadoEletronico.Infra.Shared.TestFunctions
{
	public static class OrderEntitieForUnitTest
	{
		public static Order GetOrderEntitie() => new Order
		{
			OrderId = "123456"
		};

		public static List<ItemOrder> GetItemForOrderEntitie()
		{
			var items = new List<ItemOrder>();

			items.Add(new ItemOrder
			{
				Description = "Item A",
				PriceUnit = 10,
				Qtd = 1
			});

			items.Add(new ItemOrder
			{
				Description = "Item B",
				PriceUnit = 5,
				Qtd = 2
			});

			return items;
		}
	}
}
