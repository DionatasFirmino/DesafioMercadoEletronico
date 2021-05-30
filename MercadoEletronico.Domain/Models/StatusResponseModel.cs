using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MercadoEletronico.Domain.Models
{
	public class StatusResponseModel
	{
		[JsonPropertyName("pedido")]
		public string OrderId { get; set; }
		[JsonPropertyName("status")]
		public List<string> Status { get; set; }
	}
}
