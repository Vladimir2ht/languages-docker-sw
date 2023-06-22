<h1>Star Wars Search</h1>

Для установки проекта необходимо выполнить команды:

```bash
$ git clone https://github.com/Vladimir2ht/sw-nest-docker ./StarWarsSearch
$ cd StarWarsSearch
$ npm i
```

Команда для запуска проекта в основном режиме - ``$ npm run start``
в режиме разработки - ``$ npm run dev``
и в режиме готового продукта - ``$ npm run prod``

При запуске приложения выше описанными способами, при удачном старте сервера, в консоли появится ссылка, по которой можно увидеть приложение.

Запуск приложения через Docker:
```bash
$ docker compose build
$ docker compose up
```

Другая полезная команда - пересборка контейнера:
```bash
$ docker-compose up -d --no-deps --build main
```

## License

Nest is [MIT licensed](LICENSE).
