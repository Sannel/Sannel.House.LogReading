/* Copyright 2019 Sannel Software, L.L.C.
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at
      http://www.apache.org/licenses/LICENSE-2.0
   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.*/
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sannel.House.Client;
using Sannel.House.SensorLogging.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Function
{
	public static class FunctionHandler
	{
		private static readonly HttpClientHandler handler = new HttpClientHandler()
		{
			UseCookies = false
		};

		public static async Task<IActionResult> Run(HttpRequest request, IConfiguration configuration, ILogger log)
		{
			using (var s = new JsonTextReader(new StreamReader(request.Body)))
			{
				if(log.IsEnabled(LogLevel.Debug))
				{
					log.LogDebug("New reading received");
				}
				var json = new JsonSerializer();
				var reading = json.Deserialize<SensorReading>(s);

				using(var client = new HttpClient(handler))
				{
					var house = new HouseClient(client, configuration, log);
					var result = await house.LoginAsync(configuration["Client:UserName"], configuration["Client:Password"]);
					if(result.Success)
					{
						if(log.IsEnabled(LogLevel.Debug))
						{
							log.LogDebug("Logged in with {0} given token {1} expires at {2}",
								configuration["Client:UserName"],
								house.AuthToken,
								house.ExpiresAt);
						}

						if(reading.Values?.Count > 0)
						{
							var results = await house.SensorLogging.LogReadingAsync(reading);
							if(log.IsEnabled(LogLevel.Information) && result.Success)
							{
								log.LogInformation("Reading given id {0}", result.Data);
							}
							return new OkObjectResult(results);
						}

						log.LogWarning("No values passed to log");

						var r = new Results<object>();
						r.Errors.Add("validation", new string[] { "no values passed to log" });

						return new BadRequestObjectResult(r);
					}
					else
					{
						log.LogError("Unable to login");
						return new BadRequestObjectResult(result);
					}
				}
			}
		}
	}
}
