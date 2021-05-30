using MercadoEletronico.Domain.Interfaces.Repository;
using MercadoEletronico.Domain.Interfaces.Service;
using MercadoEletronico.Domain.Models;
using MercadoEletronico.Infra.Shared.Mapper;
using System.Collections.Generic;
using System.Linq;

namespace MercadoEletronico.Service.Services
{
	public class ServiceOrder : IServiceOrder
	{
		private readonly IRepositoryOrder _repositoryOrder;
		private readonly IRepositoryItemOrder _repositoryItemOrder;

		public ServiceOrder(IRepositoryOrder repositoryOrder, IRepositoryItemOrder repositoryItemOrder)
		{
			_repositoryOrder = repositoryOrder;
			_repositoryItemOrder = repositoryItemOrder;
		}

		public void Delete(string orderId)
		{
			var items = _repositoryItemOrder.GetAll().Where(io => io.OrderId == orderId);

			foreach (var item in items)
				_repositoryItemOrder.Remove(item.Id.ToString());

			_repositoryOrder.Remove(orderId);
		}

		public IEnumerable<OrderModel> GetAll() => _repositoryOrder.GetAll().ConvertToListOrderModel()
			.Select(o => new OrderModel
			{
				OrderId = o.OrderId,
				Itens = LoadOrderItens(o.OrderId)
			});

		public OrderModel GetById(string orderId)
		{
			var order = _repositoryOrder.GetById(orderId).ConvertToOrderModel();

			if (order != null)
				order.Itens = LoadOrderItens(orderId);

			return order;
		}

		private IEnumerable<ItemOrderModel> LoadOrderItens(string orderId) =>
			 _repositoryItemOrder.GetAll().Where(io => io.OrderId == orderId).ConvertToListItemOrderModel();

		public OrderModel Insert(OrderModel order)
		{
			var orderModel = _repositoryOrder.Save(order.ConvertToOrderEntitie()).ConvertToOrderModel();

			foreach (var item in order.Itens)
			{
				item.OrderId = orderModel.OrderId;
				_repositoryItemOrder.Save(item.ConvertToItemOrderEntitie());
			}

			return orderModel;
		}

		public OrderModel Update(OrderModel order) => _repositoryOrder.Save(order.ConvertToOrderEntitie()).ConvertToOrderModel();
	}
}
