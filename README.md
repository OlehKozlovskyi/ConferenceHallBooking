# Conference Hall Booking API

REST API для управління бронюванням конференц-залів: пошук доступних залів за датою/місткістю, бронювання з розрахунком вартості оренди залежно від часу доби, та управління переліком залів і додаткових послуг (amenities).

## Технологічний стек

- **.NET 9**, ASP.NET Core Web API
- **PostgreSQL** + **EF Core** (Npgsql provider)
- **AutoMapper** — мапінг DTO ↔ Domain entities
- **FluentValidation** — автоматична валідація вхідних запитів через кастомний `IAsyncActionFilter`
- **Swagger / Swashbuckle** — інтерактивна документація API
- **Clean Architecture** — чіткий поділ на шари з дотриманням Dependency Inversion Principle

## Архітектура

Проєкт побудований за принципами **Clean Architecture** з чотирма проєктами-шарами:
- **ConferenceHallBooking.Domain # Сутності (Hall, Booking, Amenities)** — без зовнішніх залежностей
- **ConferenceHallBooking.Application** # Use cases, DTO, інтерфейси репозиторіїв/сервісів, бізнес-логіка
- **ConferenceHallBooking.Infrastructure** # EF Core, PostgreSQL, реалізації репозиторіїв
- **ConferenceHallBooking.Api** # Controllers, DI-конфігурація, Swagger, middleware

  **Напрямок залежностей:** `Api → Application → Domain`, `Infrastructure → Application → Domain`. Application не залежить від Infrastructure — інтерфейси репозиторіїв (`IBookingRepository`, `IConferenceHallRepository`) визначені в Application, а реалізації — в Infrastructure, згідно з Dependency Inversion Principle.

## Ключові архітектурні рішення

- **Автоматична валідація запитів** — `AutoValidationFilter` (глобальний `IAsyncActionFilter`) прогонить FluentValidation по аргументах action-методів через рефлексію, без ручних викликів у кожному контролері.
- **DI lifetime** — репозиторії та сервіси зареєстровані як `Scoped` (узгоджено з lifetime `DbContext`); стратегії ціноутворення — теж `Scoped` для консистентності графа залежностей.
- **UTC-обробка часу** — усі `DateTime`-значення, що йдуть у PostgreSQL (`timestamp with time zone`), нормалізуються до UTC перед записом/запитом.


  ## Swagger UI буде доступний за адресою `https://localhost:{port}/swagger`.
