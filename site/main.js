const url = 'http://[::1]:3000/get?q=';

function SendRequest() {
  let response = document.querySelector('input').value;
  console.log(url + response);
  if (!response) return;
  response = fetch(url + response);

  response
    .then((response) => {
      return response.json();
    })
    .then((response) => InsertDataIntoTables(response));
}

const tables = document.querySelectorAll('tbody');

function InsertDataIntoTables(data) {
  function InsertInTable(data, tableIndex, secondField, thirdField) {
    // Проходимся по каждому элементу и создаем строку для таблицы
    data.forEach((item, i) => {
      data[i] = `
				<tr><td>${item.name}</td><td>${item[secondField]}</td><td>${item[thirdField]}</td></tr>
			`;
    });

    tables[tableIndex].innerHTML = data.join('');
  }

  // data.people тоже можно поместить в объект.
  InsertInTable(data.people, 0, 'gender', 'mass');
  InsertInTable(data.planets, 1, 'diameter', 'population');
  InsertInTable(data.ships, 2, 'length', 'crew');
}
