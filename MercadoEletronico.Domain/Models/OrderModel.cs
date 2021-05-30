using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MercadoEletronico.Domain.Models
{
	public class OrderModel
	{
		[JsonPropertyName("pedido")]
		public string OrderId { get; set; }
		[JsonPropertyName("itens")]
		public IEnumerable<ItemOrderModel> Itens { get; set; }
	}
}
