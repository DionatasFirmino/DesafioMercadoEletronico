using System.Collections.Generic;

namespace MercadoEletronico.Domain.Interfaces.Repository
{
	public interface IRepositoryBase<T>
	{
		T Save(T obj);
		void Remove(string id);
		T GetById(string id);
		IEnumerable<T> GetAll();
	}
}
