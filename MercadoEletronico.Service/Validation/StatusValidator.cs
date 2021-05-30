using MercadoEletronico.Domain.Constants;
using MercadoEletronico.Domain.Interfaces.Service;
using MercadoEletronico.Domain.Models;
using MercadoEletronico.Infra.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MercadoEletronico.Service.Validation
{
	public class StatusValidator
	{
		private readonly IServiceOrder _serviceOrder;
		private readonly StatusRequestModel _statusRequestModel;

		public StatusResponseModel StatusResponse { get; set; }
		private OrderModel Order { get; set; }

		public StatusValidator(IServiceOrder serviceOrder, StatusRequestModel statusRequestModel)
		{
			_serviceOrder = serviceOrder;
			_statusRequestModel = statusRequestModel;
			StatusResponse = new StatusResponseModel
			{
				Status = new List<string>()
			};
		}

		public void Validate()
		{
			StatusResponse.OrderId = _statusRequestModel.OrderId;

			try
			{
				ValidateOrder();
				ValidateDisapprovedOrder();
				ValidateApprovedOrder();
				ValidateValueOfApprovedOrder();
				ValidateQuantityOfApprovedItems();
			}
			catch (Exception ex)
			{
				StatusResponse.Status.Add(ex.ReturnSimpleMessageException());
			}
		}

		private void ValidateQuantityOfApprovedItems()
		{
			var totalOrderItem = Order.Itens.Sum(io => io.Qtd);

			if (_statusRequestModel.ApprovedItems < totalOrderItem &&
				_statusRequestModel.Status.Equals(ConstantsStatus.APROVADO))
				StatusResponse.Status.Add(ConstantsStatus.APROVADO_QTD_A_MENOR);

			if (_statusRequestModel.ApprovedItems > totalOrderItem &&
				_statusRequestModel.Status.Equals(ConstantsStatus.APROVADO))
				StatusResponse.Status.Add(ConstantsStatus.APROVADO_QTD_A_MAIOR);
		}

		private void ValidateValueOfApprovedOrder()
		{
			var totalOrderValue = Order.Itens.Sum(io => io.PriceUnit * io.Qtd);

			if (_statusRequestModel.ApprovedValue < totalOrderValue &&
				_statusRequestModel.Status.Equals(ConstantsStatus.APROVADO))
				StatusResponse.Status.Add(ConstantsStatus.APROVADO_VALOR_A_MENOR);

			if (_statusRequestModel.ApprovedValue > totalOrderValue &&
				_statusRequestModel.Status.Equals(ConstantsStatus.APROVADO))
				StatusResponse.Status.Add(ConstantsStatus.APROVADO_VALOR_A_MAIOR);
		}

		private void ValidateApprovedOrder()
		{
			var totalOrderItem = Order.Itens.Sum(io => io.Qtd);
			var totalOrderValue = Order.Itens.Sum(io => io.PriceUnit * io.Qtd);

			if (_statusRequestModel.ApprovedItems == totalOrderItem &&
				_statusRequestModel.ApprovedValue == totalOrderValue &&
				_statusRequestModel.Status.Equals(ConstantsStatus.APROVADO))
				throw new Exception(ConstantsStatus.APROVADO);
		}

		private void ValidateDisapprovedOrder()
		{
			if (_statusRequestModel.Status.Equals(ConstantsStatus.REPROVADO))
				throw new Exception(ConstantsStatus.REPROVADO);
		}

		private void ValidateOrder()
		{
			Order = _serviceOrder.GetById(_statusRequestModel.OrderId);

			if (Order == null)
				throw new Exception(ConstantsStatus.CODIGO_PEDIDO_INVALIDO);
		}
	}
}
