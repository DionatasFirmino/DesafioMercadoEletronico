using MercadoEletronico.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MercadoEletronico.Infra.Data.Context
{
	public class MercadoEletronicoContext : DbContext
	{
		public MercadoEletronicoContext() { }
		public MercadoEletronicoContext(DbContextOptions<MercadoEletronicoContext> options) : base(options) { }

		public DbSet<Order> Order { get; set; }
		public DbSet<ItemOrder> ItemOrder { get; set; }
	}
}
