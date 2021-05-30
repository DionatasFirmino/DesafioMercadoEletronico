using System.Collections.Generic;

namespace MercadoEletronico.Domain.Interfaces.Service
{
	public interface IServiceBase<T>
	{
		T Insert(T dados);
		T Update(T dados);
		void Delete(string id);
		T GetById(string id);
		IEnumerable<T> GetAll();
	}
}
