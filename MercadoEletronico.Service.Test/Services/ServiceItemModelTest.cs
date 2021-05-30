using MercadoEletronico.Domain.Interfaces.Repository;
using MercadoEletronico.Domain.Interfaces.Service;
using MercadoEletronico.Domain.Models;
using MercadoEletronico.Infra.Data.Context;
using MercadoEletronico.Infra.Data.Repository;
using MercadoEletronico.Infra.Shared.TestFunctions;
using MercadoEletronico.Service.Services;
using Xunit;

namespace MercadoEletronico.Service.Test.Services
{
	public class ServiceItemModelTest
	{
		public const string NameTeste = "ServiceItemOrder{0}";

		[Fact]
		public void WhenGetItemsOrders_TheServiceMustReturnNoException()
		{
			var context = DbContextForUnitTest.GetContextForTesting(string.Format(NameTeste, "Get"));

			IServiceOrder serviceOrder = GetServiceOrder(context);
			IServiceItemOrder serviceItemOrder = GetServiceItemOrder(context);

			var exception = Record.Exception(() => serviceItemOrder.GetAll());

			Assert.Null(exception);

			var newOrder = OrderModelForUnitTest.GetOrderModel();
			serviceOrder.Insert(newOrder);

			exception = Record.Exception(() => serviceItemOrder.GetAll());
			Assert.Null(exception);

			exception = Record.Exception(() => serviceItemOrder.GetById(newOrder.OrderId));
			Assert.Null(exception);
		}

		[Fact]
		public void WhenSavingNewItemOrder_TheServiceMustReturnTheCode()
		{
			var context = DbContextForUnitTest.GetContextForTesting(string.Format(NameTeste, "Insert"));
			var order = CreateDataForTesting(context);

			IServiceItemOrder serviceItemOrder = GetServiceItemOrder(context);

			var itemModel = new ItemOrderModel
			{
				Id = "3",
				Description = "Item C",
				PriceUnit = 2,
				Qtd = 1,
				OrderId = order.OrderId
			};

			var item = serviceItemOrder.Insert(itemModel);

			Assert.NotNull(item);
			Assert.Equal("3", item.Id);
		}

		[Fact]
		public void WhenUpdateOneOrder_TheServiceMustKeepTheCode()
		{
			var context = DbContextForUnitTest.GetContextForTesting(string.Format(NameTeste, "Update"));
			var order = CreateDataForTesting(context);

			IServiceItemOrder serviceItemOrder = GetServiceItemOrder(context);

			var itemModel = new ItemOrderModel
			{
				Description = "Item C",
				PriceUnit = 2,
				Qtd = 1,
				OrderId = order.OrderId
			};

			var item = serviceItemOrder.Update(itemModel);

			Assert.NotNull(item);
			Assert.Equal("3", item.Id);
		}

		[Fact]
		public void WhenDeletedOneOrder_TheServiceMustDeleteitems()
		{
			var context = DbContextForUnitTest.GetContextForTesting(string.Format(NameTeste, "Delete"));

			CreateDataForTesting(context);

			IServiceItemOrder serviceItemOrder = GetServiceItemOrder(context);

			var exception = Record.Exception(() => serviceItemOrder.Delete("1"));

			Assert.Null(exception);
		}

		private OrderModel CreateDataForTesting(MercadoEletronicoContext context)
		{
			IServiceOrder serviceOrder = GetServiceOrder(context);

			var newOrder = OrderModelForUnitTest.GetOrderModel();

			return serviceOrder.Insert(newOrder);
		}

		private IServiceOrder GetServiceOrder(MercadoEletronicoContext context) => new ServiceOrder(GetRepositoryOrder(context), GetRepositoryItemOrder(context));

		private IServiceItemOrder GetServiceItemOrder(MercadoEletronicoContext context) => new ServiceItemOrder(GetRepositoryItemOrder(context));

		private IRepositoryItemOrder GetRepositoryItemOrder(MercadoEletronicoContext context) => new RepositoryItemOrder(context);

		private IRepositoryOrder GetRepositoryOrder(MercadoEletronicoContext context) => new RepositoryOrder(context);
	}
}
