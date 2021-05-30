using MercadoEletronico.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MercadoEletronico.Infra.CrossCutting.InversionOfControl
{
	public static class SQLiteDependency
	{

		public static void AddSQLiteDependency(this IServiceCollection services)
		{
			services.AddDbContext<MercadoEletronicoContext>(opt =>
			{
				opt.UseInMemoryDatabase("MercadoEletronico");
			});
		}
	}
}
