using MercadoEletronico.Domain.Interfaces.Repository;
using MercadoEletronico.Domain.Interfaces.Service;
using MercadoEletronico.Infra.Data.Context;
using MercadoEletronico.Infra.Data.Repository;
using MercadoEletronico.Infra.Shared.TestFunctions;
using MercadoEletronico.Service.Services;
using Xunit;

namespace MercadoEletronico.Service.Test.Services
{
	public class ServiceOrderTest
	{
		private const string NameTeste = "ServiceOrder{0}";

		[Fact]
		public void WhenGetOrders_TheServiceMustReturnNoException()
		{
			var context = DbContextForUnitTest.GetContextForTesting(string.Format(NameTeste, "Get"));

			IServiceOrder serviceOrder = GetServiceOrder(context);

			var exception = Record.Exception(() => serviceOrder.GetAll());

			Assert.Null(exception);

			var newOrder = OrderModelForUnitTest.GetOrderModel();
			serviceOrder.Insert(newOrder);

			exception = Record.Exception(() => serviceOrder.GetAll());
			Assert.Null(exception);

			exception = Record.Exception(() => serviceOrder.GetById(newOrder.OrderId));
			Assert.Null(exception);
		}

		[Fact]
		public void WhenSavingNewOrder_TheServiceMustReturnTheCode()
		{
			var context = DbContextForUnitTest.GetContextForTesting(string.Format(NameTeste, "Insert"));

			IServiceOrder serviceOrder = GetServiceOrder(context);

			var newOrder = OrderModelForUnitTest.GetOrderModel();

			var order = serviceOrder.Insert(newOrder);

			Assert.NotNull(order);
			Assert.Equal("123456", order.OrderId);
		}

		[Fact]
		public void WhenUpdateOneOrder_TheServiceMustKeepTheCode()
		{
			var context = DbContextForUnitTest.GetContextForTesting(string.Format(NameTeste, "Update"));

			IServiceOrder serviceOrder = GetServiceOrder(context);

			var newOrder = OrderModelForUnitTest.GetOrderModel();

			var order = serviceOrder.Insert(newOrder);
			order = serviceOrder.Update(order);

			Assert.Equal("123456", order.OrderId);
		}

		[Fact]
		public void WhenDeletedOneOrder_TheServiceMustDeleteitems()
		{
			var context = DbContextForUnitTest.GetContextForTesting(string.Format(NameTeste, "Delete"));

			IServiceOrder serviceOrder = GetServiceOrder(context);

			var newOrder = OrderModelForUnitTest.GetOrderModel();

			serviceOrder.Insert(newOrder);

			var order = serviceOrder.GetById(newOrder.OrderId);
			var exception = Record.Exception(() => serviceOrder.Delete(order.OrderId));

			Assert.Null(exception);
		}

		private IServiceOrder GetServiceOrder(MercadoEletronicoContext context) => new ServiceOrder(GetRepositoryOrder(context), GetRepositoryItemOrder(context));

		private IRepositoryItemOrder GetRepositoryItemOrder(MercadoEletronicoContext context) => new RepositoryItemOrder(context);

		private IRepositoryOrder GetRepositoryOrder(MercadoEletronicoContext context) => new RepositoryOrder(context);
	}
}
