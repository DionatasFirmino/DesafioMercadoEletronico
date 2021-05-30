using MercadoEletronico.Domain.Interfaces.Service;
using MercadoEletronico.Domain.Models;
using MercadoEletronico.Infra.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MercadoEletronico.Application.Controllers
{
	[Route("api/pedido")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IServiceOrder _serviceOrder;

		public OrderController(IServiceOrder serviceOrder) => _serviceOrder = serviceOrder;

		// GET: api/pedido
		[HttpGet]
		public IActionResult Get()
		{
			try
			{
				var orders = _serviceOrder.GetAll();

				return Ok(orders);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.ReturnSimpleMessageException());
			}
		}

		// GET api/pedido/5
		[HttpGet("{id}")]
		public IActionResult Get(string id)
		{
			try
			{
				var order = _serviceOrder.GetById(id);

				return Ok(order);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.ReturnSimpleMessageException());
			}
		}

		// POST api/pedido
		[HttpPost]
		public IActionResult Post([FromBody] OrderModel orderModel)
		{
			try
			{
				var order = _serviceOrder.Insert(orderModel);

				return Ok(order?.OrderId);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.ReturnSimpleMessageException());
			}
		}

		// PUT api/pedido/5
		[HttpPut("{id}")]
		public IActionResult Put(string id, [FromBody] OrderModel orderModel)
		{
			try
			{
				if (id != orderModel.OrderId)
					throw new Exception("Item não encontrado.");

				var order = _serviceOrder.Update(orderModel);

				return Ok(order);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		// DELETE api/pedido/5
		[HttpDelete("{id}")]
		public IActionResult Delete(string id)
		{
			try
			{
				_serviceOrder.Delete(id);

				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.ReturnSimpleMessageException());
			}
		}
	}
}
