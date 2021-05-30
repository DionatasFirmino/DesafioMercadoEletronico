using MercadoEletronico.Domain.Entities;
using MercadoEletronico.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace MercadoEletronico.Infra.Shared.Mapper
{
	public static class ItemOrderMapper
	{
		public static ItemOrder ConvertToItemOrderEntitie(this ItemOrderModel itemOrderModel) =>
			itemOrderModel == null ? null : new ItemOrder
			{
				Id = itemOrderModel.Id,
				Description = itemOrderModel.Description,
				PriceUnit = itemOrderModel.PriceUnit,
				Qtd = itemOrderModel.Qtd,
				OrderId = itemOrderModel.OrderId,
				Order = itemOrderModel.Order.ConvertToOrderEntitie()
			};

		public static ItemOrderModel ConvertToItemOrderModel(this ItemOrder itemOrder) =>
			itemOrder == null ? null : new ItemOrderModel
			{
				Id = itemOrder.Id,
				Description = itemOrder.Description,
				PriceUnit = itemOrder.PriceUnit,
				Qtd = itemOrder.Qtd,
				OrderId = itemOrder.OrderId,
				Order = itemOrder.Order.ConvertToOrderModel()
			};

		public static IEnumerable<ItemOrderModel> ConvertToListItemOrderModel(this IEnumerable<ItemOrder> itemOrders) =>
			new List<ItemOrderModel>(itemOrders.Select(io => io.ConvertToItemOrderModel()));
	}
}
