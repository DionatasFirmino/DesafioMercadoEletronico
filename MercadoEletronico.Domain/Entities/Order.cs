using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoEletronico.Domain.Entities
{
	[Table("Order")]
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public string OrderId { get; set; }
	}
}
