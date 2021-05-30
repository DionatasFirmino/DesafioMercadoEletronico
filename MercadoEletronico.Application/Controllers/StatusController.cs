using MercadoEletronico.Domain.Interfaces.Service;
using MercadoEletronico.Domain.Models;
using MercadoEletronico.Service.Validation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MercadoEletronico.Application.Controllers
{
	[Route("api/status")]
	[ApiController]
	public class StatusController : ControllerBase
	{
		private readonly IServiceOrder _serviceOrder;
		public StatusController(IServiceOrder serviceOrder) => _serviceOrder = serviceOrder;

		// POST api/status
		[HttpPost]
		public IActionResult Post([FromBody] StatusRequestModel statusRequest)
		{
			var validator = new StatusValidator(_serviceOrder, statusRequest);
			validator.Validate();
			return Ok(validator.StatusResponse);
		}
	}
}
