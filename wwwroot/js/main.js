const url = "http://localhost:3000/get?q=";

function SendRequest() {

	let reqOrRes = document.querySelector('input').value;
	console.log(url + reqOrRes);
	if (!reqOrRes) return;
	reqOrRes = fetch(url + reqOrRes);

	reqOrRes.then(response => response.json())
		.then(response => InsertDataIntoTables(response));
};

const tables = document.querySelectorAll('tbody');

function InsertDataIntoTables(data) {

	// Проходимся по каждому элементу и создаем строку для таблицы
	data.people.forEach((item, i) => {
		data.people[i] =
			`<tr><td>${item.name}</td><td>${item.gender}</td><td>${item.mass}</td></tr>`;
	});

	tables[0].innerHTML = data.people.join('');

	data.planets.forEach((item, i) => {
		data.planets[i] =
			`<tr><td>${item.name}</td><td>${item.diameter}</td><td>${item.population}</td></tr>`;
	});

	tables[1].innerHTML = data.planets.join('');

	data.ships.forEach((item, i) => {
		data.ships[i] =
			`<tr><td>${item.name}</td><td>${item.length}</td><td>${item.crew}</td></tr>`;
	});

	tables[2].innerHTML = data.ships.join('');
}