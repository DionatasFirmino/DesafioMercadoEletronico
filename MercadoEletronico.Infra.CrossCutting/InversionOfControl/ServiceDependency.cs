using MercadoEletronico.Domain.Interfaces.Service;
using MercadoEletronico.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MercadoEletronico.Infra.CrossCutting.InversionOfControl
{
	public static class ServiceDependency
	{
		public static void AddServiceDependency(this IServiceCollection services)
		{
			services.AddScoped<IServiceOrder, ServiceOrder>();
			services.AddScoped<IServiceItemOrder, ServiceItemOrder>();
		}
	}
}
