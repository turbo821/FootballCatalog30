# FootballCatalog30

## Аннотация
Web-приложение «Каталог футболистов 3.0». Язык разработки – C#. База данных — PostgreSQL. Для backend используется ASP.NET Core 8. Для frontend -- React. 

Приложение состоит из двух страниц. На страницах располагается переключатель между ними.

### Первая страница 

Предоставляет пользователю функционал добавления футболистов и содержит следующие поля:
1.  Имя
2.  Фамилия
3.  Пол
4.  Дата рождения
5.  Название команды
6.  Страна (список стран фиксированный и не подлежит изменению пользователями системы: Россия, США, Италия)

При этом поле «название команды» – позволяет выбрать одну из уже добавленных ранее команд, а если такой команды еще не было добавлено, то, добавить её непосредственно здесь (не переходя на другую страницу).

### Вторая страница 

Отображает актуальный список добавленных в систему футболистов. Возле каждого футболиста должна присутствует кнопка редактирования, обеспечивающая возможность изменить данные по выбранному футболисту.

Данные на этой странице отображаются в режиме реального времени с помощью SignalR. Т. е. при внесении новой записи любым из пользователей приложения, изменения отображаются здесь сразу же, без перезагрузки страницы.

## Описание файлов

- FootballCatalog30.Api - api aspnet core
- football-catalog-30-client - клиент react
- postgres_data - появляется после запуска docker-compose, здесь хранятся данные postgres volumes
- footballdb_dump.sql - используется для дампа БД при первом запуске docker-compose

## Как запустить в Docker

Перейдя в директорию репозитория написать команду:
`docker-compose up --build`

После запуска клиент будет доступен по адресу `http://localhost:3000`, по адресу `http://localhost:8080/swagger` можно протестировать api.

Для удаления контейнеров написать команду:
`docker-compose down`
