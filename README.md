# AbiturientTracker

Сервис для отслеживания рейтинга абитуриентов и анализа конкурсных списков.

Проект получает данные из конкурсных списков ТУСУР, распределяет абитуриентов по направлениям и рассчитывает статистику по конкурсу.

## Источник данных

Данные получаются из официальных конкурсных списков ТУСУР:

https://contest.tusur.ru/#/campaigns/bachelor/postal

## Запуск
### Docker

Для запуска проекта требуется Docker.

```bash
docker compose up --build
```

После запуска:

- Web-интерфейс: http://localhost:3000
- API: http://localhost:8080/api

## Стек

- C# / ASP.NET Core Web API
- HTML / CSS / JavaScript
- Docker
- Nginx