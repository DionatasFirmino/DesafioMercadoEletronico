using MercadoEletronico.Domain.Interfaces.Repository;
using MercadoEletronico.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace MercadoEletronico.Infra.CrossCutting.InversionOfControl
{
	public static class RepositoryDependency
	{
		public static void AddRepositoryDependency(this IServiceCollection services)
		{
			services.AddScoped<IRepositoryOrder, RepositoryOrder>();
			services.AddScoped<IRepositoryItemOrder, RepositoryItemOrder>();
		}
	}
}
