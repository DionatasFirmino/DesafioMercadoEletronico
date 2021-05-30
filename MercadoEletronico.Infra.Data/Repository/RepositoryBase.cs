using MercadoEletronico.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MercadoEletronico.Infra.Data.Repository
{
	public class RepositoryBase<TEntity> where TEntity : class
	{
		protected readonly MercadoEletronicoContext _mercadoEletronicoContext;

		public RepositoryBase(MercadoEletronicoContext mercadoEletronicoContext) =>
			_mercadoEletronicoContext = mercadoEletronicoContext;

		protected virtual TEntity Insert(TEntity obj)
		{
			_mercadoEletronicoContext.Set<TEntity>().Add(obj);
			_mercadoEletronicoContext.SaveChanges();

			return obj;
		}

		protected virtual TEntity Update(TEntity obj)
		{
			var local = _mercadoEletronicoContext.Set<TEntity>().Local.ToList();

			if (local != null)
				foreach (var item in local)
					_mercadoEletronicoContext.Entry(item).State = EntityState.Detached;

			_mercadoEletronicoContext.Set<TEntity>().Add(obj).State = EntityState.Modified;
			_mercadoEletronicoContext.SaveChanges();

			return obj;
		}

		protected virtual void Delete(string id)
		{
			_mercadoEletronicoContext.Set<TEntity>().Remove(Select(id));
			_mercadoEletronicoContext.SaveChanges();
		}

		protected virtual IEnumerable<TEntity> Select() =>
			_mercadoEletronicoContext.Set<TEntity>().ToList();

		protected virtual TEntity Select(string id) =>
			_mercadoEletronicoContext.Set<TEntity>().Find(id);
	}
}
