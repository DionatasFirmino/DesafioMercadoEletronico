using MercadoEletronico.Domain.Constants;
using MercadoEletronico.Domain.Interfaces.Repository;
using MercadoEletronico.Domain.Interfaces.Service;
using MercadoEletronico.Domain.Models;
using MercadoEletronico.Infra.Data.Context;
using MercadoEletronico.Infra.Data.Repository;
using MercadoEletronico.Infra.Shared.TestFunctions;
using MercadoEletronico.Service.Services;
using MercadoEletronico.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MercadoEletronico.Service.Test.Validation
{
	public class StatusValidatorTest
	{
		public const string NameTeste = "ServiceOrder{0}";

		[Fact]
		public void IfOrderCordeNotFoundReturnInvalidCode()
		{
			IServiceOrder serviceOrder = CreateDataForTesting();

			var request = new StatusRequestModel
			{
				Status = "APROVADO",
				ApprovedItems = 3,
				ApprovedValue = 20,
				OrderId = "123456-N"
			};

			var validator = new StatusValidator(serviceOrder, request);
			validator.Validate();

			Assert.Equal(ConstantsStatus.CODIGO_PEDIDO_INVALIDO, validator.StatusResponse.Status[0]);
		}

		[Fact]
		public void IfOrderStautsIsDisapprovedReturnThatTheOrderIsDesapproved()
		{
			IServiceOrder serviceOrder = CreateDataForTesting();

			var request = new StatusRequestModel
			{
				Status = "REPROVADO",
				ApprovedItems = 0,
				ApprovedValue = 0,
				OrderId = "123456"
			};

			var validator = new StatusValidator(serviceOrder, request);
			validator.Validate();

			Assert.Equal(ConstantsStatus.REPROVADO, validator.StatusResponse.Status[0]);
		}

		[Fact]
		public void IfApprovedStatusAndItemQuantityEqualToApprovedAndTotalOrderValueEqualToThatOfTheItemReturnApproved()
		{
			IServiceOrder serviceOrder = CreateDataForTesting();

			var request = new StatusRequestModel
			{
				Status = "APROVADO",
				ApprovedItems = 3,
				ApprovedValue = 20,
				OrderId = "123456"
			};

			var validator = new StatusValidator(serviceOrder, request);
			validator.Validate();

			Assert.Equal(ConstantsStatus.APROVADO, validator.StatusResponse.Status[0]);
		}

		[Fact]
		public void IfLocalizedOrderAndApprovadValueIsLessTheanTheTotalOrderValueAndTheSameApprovedOrderReturnApprovedLowerValue()
		{
			IServiceOrder serviceOrder = CreateDataForTesting();

			var request = new StatusRequestModel
			{
				Status = "APROVADO",
				ApprovedItems = 3,
				ApprovedValue = 10,
				OrderId = "123456"
			};

			var validator = new StatusValidator(serviceOrder, request);
			validator.Validate();

			Assert.Equal(ConstantsStatus.APROVADO_VALOR_A_MENOR , validator.StatusResponse.Status[0]);
		}

		[Fact]
		public void IfLocalizedOrderAndTheApprovedQuantityIsLessThanTheTotalOrderQuantityAndTheSameApprovedOrderReturnApprovedLesserQuantity()
		{
			IServiceOrder serviceOrder = CreateDataForTesting();

			var request = new StatusRequestModel
			{
				Status = "APROVADO",
				ApprovedItems = 2,
				ApprovedValue = 20,
				OrderId = "123456"
			};

			var validator = new StatusValidator(serviceOrder, request);
			validator.Validate();

			Assert.Equal(ConstantsStatus.APROVADO_QTD_A_MENOR, validator.StatusResponse.Status[0]);
		}

		[Fact]
		public void IfLocalizedOrderAndApprovedValueIsFreaterThanTheTotalValueOfTheOrderAndEqualOrderApprovedReturnApprovedValueGreater()
		{
			IServiceOrder serviceOrder = CreateDataForTesting();

			var request = new StatusRequestModel
			{
				Status = "APROVADO",
				ApprovedItems = 3,
				ApprovedValue = 21,
				OrderId = "123456"
			};

			var validator = new StatusValidator(serviceOrder, request);
			validator.Validate();

			Assert.Equal(ConstantsStatus.APROVADO_VALOR_A_MAIOR, validator.StatusResponse.Status[0]);
		}

		[Fact]
		public void IfLocalizedOrderAndApprovedQuantitYIsFreaterThanTheTotalQuantityOfTheOrderAndEqualOrderApprovedReturnApprovedQuantityGreater()
		{
			IServiceOrder serviceOrder = CreateDataForTesting();

			var request = new StatusRequestModel
			{
				Status = "APROVADO",
				ApprovedItems = 4,
				ApprovedValue = 20,
				OrderId = "123456"
			};

			var validator = new StatusValidator(serviceOrder, request);
			validator.Validate();

			Assert.Equal(ConstantsStatus.APROVADO_QTD_A_MAIOR, validator.StatusResponse.Status[0]);
		}

		private IServiceOrder CreateDataForTesting()
		{
			var context = DbContextForUnitTest.GetContextForTesting(string.Format(NameTeste, "Validator"));

			IServiceOrder serviceOrder = GetServiceOrder(context);

			var newOrder = OrderModelForUnitTest.GetOrderModel();

			serviceOrder.Insert(newOrder);

			return serviceOrder;
		}

		private IServiceOrder GetServiceOrder(MercadoEletronicoContext context) => new ServiceOrder(GetRepositoryOrder(context), GetRepositoryItemOrder(context));

		private IRepositoryItemOrder GetRepositoryItemOrder(MercadoEletronicoContext context) => new RepositoryItemOrder(context);

		private IRepositoryOrder GetRepositoryOrder(MercadoEletronicoContext context) => new RepositoryOrder(context);

	}
}
