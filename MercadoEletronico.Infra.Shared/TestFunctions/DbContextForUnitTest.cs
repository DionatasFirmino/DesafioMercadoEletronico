using MercadoEletronico.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MercadoEletronico.Infra.Shared.TestFunctions
{
	public static class DbContextForUnitTest
	{
		public static MercadoEletronicoContext GetContextForTesting(string nameTeste)
		{
			DbContextOptions<MercadoEletronicoContext> options;

			var builder = new DbContextOptionsBuilder<MercadoEletronicoContext>();
			builder.UseInMemoryDatabase($"MercadoEletronico_{nameTeste}");

			options = builder.Options;

			MercadoEletronicoContext mercadoEletronicoContext = new MercadoEletronicoContext(options);
			mercadoEletronicoContext.Database.EnsureDeleted();
			mercadoEletronicoContext.Database.EnsureCreated();

			return mercadoEletronicoContext;
		}
	}
}
