using MercadoEletronico.Domain.Entities;
using MercadoEletronico.Domain.Interfaces.Repository;
using MercadoEletronico.Infra.Data.Repository;
using MercadoEletronico.Infra.Shared.TestFunctions;
using Xunit;

namespace MercadoEletronico.Infra.Data.Test.Repository
{
	public class RepositoryOrderTest
	{
		[Fact]
		public void WhenAddingNewOrdermMustReturnTheCode()
		{
			IRepositoryOrder repositoryOrder = new RepositoryOrder(DbContextForUnitTest.GetContextForTesting("Order"));

			var newOrder = new Order { OrderId = "123456" };

			var order = repositoryOrder.Save(newOrder);

			Assert.NotNull(order);
			Assert.Equal("123456", order.OrderId);
		}
	}
}
