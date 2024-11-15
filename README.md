<h1>Star Wars Search</h1>

Для установки проекта необходимо выполнить команды:

```bash
$ git clone https://github.com/Vladimir2ht/sw-nest-docker ./StarWarsSearch
$ cd StarWarsSearch
```

Команда для запуска проекта локально - ``dotnet run``

При запуске приложения выше описанными способами, при удачном старте сервера, в консоли появится ссылка, по которой можно увидеть приложение.

Запуск приложения через Docker:
```bash
$ docker compose up
```

Другая полезная команда - пересборка контейнера и образа:
```bash
$ docker-compose up -d --no-deps --build main
```