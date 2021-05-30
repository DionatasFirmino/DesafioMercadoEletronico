using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoEletronico.Domain.Entities
{
	[Table("ItemOrder")]
	public class ItemOrder
	{
		[Key]
		public string Id { get; set; }
		public string Description { get; set; }
		public double PriceUnit { get; set; }
		public int Qtd { get; set; }
		
		public string OrderId { get; set; }
		public virtual Order Order { get; set; }
	}
}
