using System.Text.Json;
using System.Text.Json.Serialization;

namespace MercadoEletronico.Domain.Models
{
	public class ItemOrderModel
	{
		public string Id { get; set; }
		[JsonPropertyName("descricao")]
		public string Description { get; set; }
		[JsonPropertyName("precoUnitario")]
		public double PriceUnit { get; set; }
		[JsonPropertyName("qtd")]
		public int Qtd { get; set; }

		[JsonPropertyName("pedido")]
		public string OrderId { get; set; }
		[JsonIgnore]
		public virtual OrderModel Order { get; set; }
	}
}
