using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Function.Tests
{
	public class FunctionHandlerTest
	{
		[Fact]
		public async Task HandlerAsyncTest()
		{
			IConfigurationBuilder builder = new ConfigurationBuilder();

			var configuration = builder.Build();

			var loggerFactory = new LoggerFactory();
			var logger = loggerFactory.CreateLogger<FunctionHandlerTest>();

			var context = new DefaultHttpContext();
			using(var ms = new MemoryStream())
			{
				var writer = new StreamWriter(ms);
/*				writer.Write("Test Data");
				writer.Flush();
				ms.Seek(0, SeekOrigin.Begin);

				context.Request.Body = ms;

				var result = await Function.FunctionHandler.Run(context.Request, configuration, logger);

				var r = Assert.IsAssignableFrom<OkObjectResult>(result);
				Assert.Equal("Function received content Test Data", r.Value as string);
				*/
			}

		}
	}
}
