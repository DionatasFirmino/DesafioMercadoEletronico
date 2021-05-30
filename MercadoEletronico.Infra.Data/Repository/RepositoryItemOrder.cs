using MercadoEletronico.Domain.Entities;
using MercadoEletronico.Domain.Interfaces.Repository;
using MercadoEletronico.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MercadoEletronico.Infra.Data.Repository
{
	public class RepositoryItemOrder : RepositoryBase<ItemOrder>, IRepositoryItemOrder
	{
		public RepositoryItemOrder(MercadoEletronicoContext mercadoEletronicoContext) : base(mercadoEletronicoContext) { }

		public IEnumerable<ItemOrder> GetAll() => base.Select();

		public ItemOrder GetById(string id) => base.Select(id);

		public void Remove(string id) => base.Delete(id);

		public ItemOrder Save(ItemOrder itemOrder)
		{
			if (GetById(itemOrder.Id) == null)
			{
				var lastItem = GetAll().OrderByDescending(i => i.Id).FirstOrDefault()?.Id;

				if (lastItem != null)
					itemOrder.Id = (Convert.ToInt32(lastItem) + 1).ToString();
				else
					itemOrder.Id = "1";
				return base.Insert(itemOrder);
			}
			else
				return base.Update(itemOrder);
		}
	}
}
