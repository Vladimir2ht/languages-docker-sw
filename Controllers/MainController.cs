using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace star_wars.Controllers
{
	public class MainController : Controller
	{
		private readonly IMemoryCache _cache;
		private readonly HttpClient _httpClient;

		public MainController(IMemoryCache cache)
		{
			_cache = cache;
			_httpClient = new HttpClient
			{
				BaseAddress = new Uri("https://swapi.dev/api")
			};
		}

		public ActionResult Index()
		{
			Console.WriteLine("HTML");
			return View();
		}

		private async Task<List<T>> GetEntitiesAsync<T>(string entity)
		{
			return await _cache.GetOrCreateAsync(entity, async entry =>
				{
					entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(60); // Cache for 60 Hours
					Console.WriteLine("request");

					List<T> results = new List<T>();
					var nextPageUrl = $"https://swapi.dev/api/{entity}/";

					while (!string.IsNullOrEmpty(nextPageUrl))
					{
						var response = await _httpClient.GetFromJsonAsync<ResponseBody<T>>(nextPageUrl);

						if (response == null) continue;

						var entities = response?.results ?? new List<T>();
						results.AddRange(entities);
						nextPageUrl = response?.next;
					}

					return results;
				});
		}

		[HttpGet]
		public async Task<Result> Get([FromQuery] string q)
		{
			var people = (await GetEntitiesAsync<Person>("people"))
				.FindAll(e => e.Name.ToLower().Contains(q.ToLower()))
				.ConvertAll(el => new Person { Name = el.Name, Gender = el.Gender, Mass = el.Mass });

			var planets = (await GetEntitiesAsync<Planet>("planets"))
				.FindAll(e => e.Name.ToLower().Contains(q.ToLower()))
				.ConvertAll(el => new Planet { Name = el.Name, Population = el.Population, Diameter = el.Diameter });

			var ships = (await GetEntitiesAsync<Ship>("starships"))
				.FindAll(e => e.Name.ToLower().Contains(q.ToLower()))
				.ConvertAll(el => new Ship { Name = el.Name, Length = el.Length, Crew = el.Crew });

			return new Result
			{
				People = people,
				Planets = planets,
				Ships = ships
			};
		}
	}

	public class Result
	{
		public required List<Person> People { get; set; }
		public required List<Planet> Planets { get; set; }
		public required List<Ship> Ships { get; set; }
	}

	public class Person
	{
		public required string Name { get; set; }
		public required string Gender { get; set; }
		public required string Mass { get; set; }
	}

	public class Planet
	{
		public required string Name { get; set; }
		public required string Population { get; set; }
		public required string Diameter { get; set; }
	}

	public class Ship
	{
		public required string Name { get; set; }
		public required string Length { get; set; }
		public required string Crew { get; set; }
	}

	public class UnknownEntity
	{
		public required string Name { get; set; }
		public string? Gender { get; set; }
		public string? Mass { get; set; }
		public string? Population { get; set; }
		public string? Diameter { get; set; }
		public string? Length { get; set; }
		public string? Crew { get; set; }
	}

	public class ResponseBody<T>
	{
		public List<T>? results { get; set; }
		public string? next { get; set; }
	}
}