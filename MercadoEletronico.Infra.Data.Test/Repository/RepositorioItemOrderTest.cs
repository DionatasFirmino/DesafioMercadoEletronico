using MercadoEletronico.Domain.Entities;
using MercadoEletronico.Domain.Interfaces.Repository;
using MercadoEletronico.Infra.Data.Repository;
using MercadoEletronico.Infra.Shared.TestFunctions;
using System.Collections.Generic;
using Xunit;

namespace MercadoEletronico.Infra.Data.Test.Repository
{
	public class RepositorioItemOrderTest
	{
		[Fact]
		public void WhenAddingNewOrderWithItemMustAddItems()
		{
			var context = DbContextForUnitTest.GetContextForTesting("ItemOrder");

			IRepositoryOrder repositoryOrder = new RepositoryOrder(context);
			IRepositoryItemOrder repositoryItemOrder = new RepositoryItemOrder(context);

			var newOrder = OrderEntitieForUnitTest.GetOrderEntitie();
			var listItemsOrder = OrderEntitieForUnitTest.GetItemForOrderEntitie();

			var order = repositoryOrder.Save(newOrder);

			Assert.NotNull(order);

			var id = 0;

			foreach (var item in listItemsOrder)
			{
				item.OrderId = order.OrderId;
				repositoryItemOrder.Save(item);

				id++;

				Assert.Equal(id.ToString(), item.Id);
			}
		}
	}
}
