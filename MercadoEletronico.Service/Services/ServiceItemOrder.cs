using MercadoEletronico.Domain.Interfaces.Repository;
using MercadoEletronico.Domain.Interfaces.Service;
using MercadoEletronico.Domain.Models;
using MercadoEletronico.Infra.Shared.Mapper;
using System.Collections.Generic;

namespace MercadoEletronico.Service.Services
{
	public class ServiceItemOrder : IServiceItemOrder
	{
		private readonly IRepositoryItemOrder _repositoryItemOrder;
		public ServiceItemOrder(IRepositoryItemOrder repositoryItemOrder) => _repositoryItemOrder = repositoryItemOrder;

		public void Delete(string itemOrderId) => _repositoryItemOrder.Remove(itemOrderId);

		public IEnumerable<ItemOrderModel> GetAll() => _repositoryItemOrder.GetAll().ConvertToListItemOrderModel();

		public ItemOrderModel GetById(string itemOrderId) => _repositoryItemOrder.GetById(itemOrderId).ConvertToItemOrderModel();

		public ItemOrderModel Insert(ItemOrderModel itemOrder) => _repositoryItemOrder.Save(itemOrder.ConvertToItemOrderEntitie()).ConvertToItemOrderModel();

		public ItemOrderModel Update(ItemOrderModel itemOrder) => _repositoryItemOrder.Save(itemOrder.ConvertToItemOrderEntitie()).ConvertToItemOrderModel();
	}
}
