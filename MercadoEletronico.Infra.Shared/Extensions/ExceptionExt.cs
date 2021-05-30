using System;

namespace MercadoEletronico.Infra.Shared.Extensions
{
	public static class ExceptionExt
	{
		public static string ReturnSimpleMessageException(this Exception exception)
		{
			var exceptionBase = exception.GetBaseException();

			if (exceptionBase == null)
				return exception.InnerException != null ? exception.InnerException.Message : exception.Message;

			return exceptionBase.Message;
		}
	}
}
