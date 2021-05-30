using MercadoEletronico.Domain.Entities;
using MercadoEletronico.Domain.Interfaces.Repository;
using MercadoEletronico.Infra.Data.Context;
using System.Collections.Generic;

namespace MercadoEletronico.Infra.Data.Repository
{
	public class RepositoryOrder : RepositoryBase<Order>, IRepositoryOrder
	{
		public RepositoryOrder(MercadoEletronicoContext mercadoEletronicoContext) : base(mercadoEletronicoContext) { }

		public IEnumerable<Order> GetAll() => base.Select();

		public Order GetById(string id) => base.Select(id);

		public void Remove(string id) => base.Delete(id);

		public Order Save(Order order)
		{
			if (GetById(order.OrderId) == null)
				return base.Insert(order);
			else
				return base.Update(order);
		}
	}
}
