using System.Text.Json.Serialization;

namespace MercadoEletronico.Domain.Models
{
	public class StatusRequestModel
	{
		[JsonPropertyName("status")]
		public string Status { get; set; }
		[JsonPropertyName("itensAprovados")]
		public int ApprovedItems { get; set; }
		[JsonPropertyName("valorAprovado")]
		public int ApprovedValue { get; set; }
		[JsonPropertyName("pedido")]
		public string OrderId { get; set; }
	}
}
