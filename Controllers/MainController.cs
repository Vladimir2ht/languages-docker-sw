using Microsoft.AspNetCore.Mvc;

namespace star_wars.Controllers
{
	public class MainController : Controller
	{
		public ActionResult Index()
		{
			Console.WriteLine("HTML");
			return View();
		}

		private readonly HttpClient httpClient = new()
		{
			BaseAddress = new Uri("https://swapi.dev/api"),
		};

		[HttpGet]
		public async Task<Result> Get([FromQuery] string q)
		{

			var peopleTask = SearchEntitiesAsync<Person>("people", q);
			var planetsTask = SearchEntitiesAsync<Planet>("planets", q);
			var shipsTask = SearchEntitiesAsync<Ship>("starships", q);

			await Task.WhenAll(peopleTask, planetsTask, shipsTask);

			var people = peopleTask.Result;
			var planets = planetsTask.Result;
			var ships = shipsTask.Result;

			return new Result
			{
				People = people.ConvertAll(el => new Person { Name = el.Name, Gender = el.Gender, Mass = el.Mass }),
				Planets = planets.ConvertAll(el => new Planet { Name = el.Name, Population = el.Population, Diameter = el.Diameter }),
				Ships = ships.ConvertAll(el => new Ship { Name = el.Name, Length = el.Length, Crew = el.Crew })
			};
		}

		private async Task<List<UnknownEntity>> SearchEntitiesAsync<T>(string entity, string query)
		{
			string url = $"https://swapi.dev/api/{entity}/";
			var results = new List<UnknownEntity>();
			var nextPageUrl = url;

			while (!string.IsNullOrEmpty(nextPageUrl))
			{
				var response = await httpClient.GetFromJsonAsync<ResponseBody<UnknownEntity>>(nextPageUrl);

				if (response == null) continue;

				var entities = response?.results?
				.FindAll(e => e.Name != null ? e.Name.ToLower().Contains(query.ToLower()) : false)
					?? new List<UnknownEntity>();
				results.AddRange(entities);
				nextPageUrl = response?.next;
			}

			return results;
		}
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