PetFamily 🐾
https://img.shields.io/badge/.NET-8.0-blue
https://img.shields.io/badge/ASP.NET_Core-8.0-blueviolet
https://img.shields.io/badge/PostgreSQL-16.0-blue
https://img.shields.io/badge/Docker-%E2%9C%93-blue
https://img.shields.io/badge/Swagger-%E2%9C%93-green
https://img.shields.io/badge/Seq-Logging-orange
https://img.shields.io/badge/MinIO-S3%2520Storage-yellow
https://img.shields.io/badge/license-MIT-green

PetFamily - это платформа для поиска новых хозяев бездомным животным. Система позволяет волонтерам публиковать информацию о животных, нуждающихся в доме, а потенциальным хозяевам - находить питомцев и подавать заявки на adoption.

✨ Возможности
📝 Публикация объявлений о животных с фотографиями (хранение в MinIO)

🔍 Поиск и фильтрация животных по различным критериям

📋 Система заявок на adoption с валидацией FluentValidation

👤 Управление профилями волонтеров и пользователей

🔐 JWT аутентификация и авторизация

📊 Административная панель управления

📖 Автоматическая документация API через Swagger

📝 Централизованное логирование в Seq

🏗️ Архитектура
Проект построен по принципам Clean Architecture и включает:

Domain Layer - бизнес-логика и сущности

Application Layer - use cases и CQRS

Infrastructure Layer - работа с БД, внешние сервисы

API Layer - Web API контроллеры

🛠️ Технологический стек
Backend
.NET 8 - основная платформа

ASP.NET Core Web API - REST API

Entity Framework Core 8 - ORM

PostgreSQL - основная база данных

Redis - кэширование

Hangfire - фоновая обработка задач

Serilog - структурированное логирование

Seq - система просмотра и анализа логов

FluentValidation - валидация входных данных

MediatR - медиатор паттерн (CQRS)

AutoMapper - маппинг объектов

Swagger/OpenAPI - документация API

MinIO - S3-совместимое хранилище файлов

Инфраструктура
Docker - контейнеризация

Docker Compose - оркестрация

Nginx - reverse proxy

GitHub Actions - CI/CD

🚀 Быстрый старт
Предварительные требования
.NET 8.0 SDK

Docker Desktop

PostgreSQL 16+ (опционально)

Запуск через Docker (рекомендуется)
bash
# Клонирование репозитория
git clone https://github.com/aleksey-sosnovcev/PetFamily.git
cd PetFamily

# Запуск всех сервисов
docker-compose up -d

# Остановка
docker-compose down
Сервисы будут доступны по адресам:

API: http://localhost:5000

Swagger UI: http://localhost:5000/swagger

Seq Logs: http://localhost:5341

MinIO Console: http://localhost:9001

pgAdmin: http://localhost:5050

Hangfire Dashboard: http://localhost:5000/hangfire

Локальная разработка
Backend
bash
cd src/PetFamily.API

# Восстановление зависимостей
dotnet restore

# Настройка базы данных
dotnet ef database update

# Запуск
dotnet run
API будет доступен с Swagger документацией по адресу: https://localhost:7000/swagger

⚙️ Конфигурация
Создайте файл appsettings.Development.json:

json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=PetFamilyDB;Username=postgres;Password=password",
    "Redis": "localhost:6379"
  },
  "Jwt": {
    "Key": "your-super-secure-jwt-key-minimum-32-chars",
    "Issuer": "PetFamily",
    "Audience": "PetFamilyUsers",
    "ExpiryInMinutes": 60
  },
  "MinIO": {
    "Endpoint": "localhost:9000",
    "AccessKey": "minioadmin",
    "SecretKey": "minioadmin",
    "BucketName": "petfamily",
    "WithSSL": false
  },
  "Seq": {
    "ServerUrl": "http://localhost:5341",
    "ApiKey": "optional-api-key"
  },
  "Logging": {
    "Seq": {
      "MinimumLevel": "Information",
      "UseDefaultLevel": false
    }
  }
}
🔍 Документация API
Проект использует Swagger для автоматической генерации документации API:

Swagger UI: http://localhost:5000/swagger

OpenAPI спецификация: http://localhost:5000/swagger/v1/swagger.json

Примеры валидации с FluentValidation
csharp
public class CreatePetValidator : AbstractValidator<CreatePetRequest>
{
    public CreatePetValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя животного обязательно")
            .MaximumLength(100).WithMessage("Имя не должно превышать 100 символов");
        
        RuleFor(x => x.Age)
            .GreaterThan(0).WithMessage("Возраст должен быть положительным числом");
        
        RuleFor(x => x.Photos)
            .Must(photos => photos.Count <= 10)
            .WithMessage("Не более 10 фотографий на животное");
    }
}
📊 Логирование
Проект использует структурированное логирование с Serilog и Seq:

csharp
// Пример логирования в коде
_logger.LogInformation("Пользователь {UserId} создал новое животное {PetId}", 
    userId, petId);

_logger.LogWarning("Неудачная попытка входа для пользователя {Username}", 
    username);

_logger.LogError(ex, "Ошибка при сохранении файла в MinIO для животного {PetId}", 
    petId);
Для просмотра логов откройте Seq: http://localhost:5341

📁 Хранение файлов
Проект использует MinIO для хранения файлов:

Хранение фотографий животных

Резервные копии документов

S3-совместимое API

Автоматическое создание бакетов

Работа с MinIO
csharp
// Загрузка файла в MinIO
await _minioService.UploadFileAsync(bucketName, objectName, stream, contentType);

// Получение ссылки на файл
var url = await _minioService.GetPresignedUrlAsync(bucketName, objectName);
📁 Структура проекта
text
PetFamily/
├── src/
│   ├── PetFamily.API/           # Web API проект (Swagger, контроллеры)
│   ├── PetFamily.Application/   # Use cases, CQRS, FluentValidation
│   ├── PetFamily.Domain/        # Доменные сущности
│   ├── PetFamily.Infrastructure/ # Репозитории, сервисы (MinIO, Seq)
├── tests/
│   ├── PetFamily.UnitTests/     # Юнит-тесты (включая валидацию)
│   └── PetFamily.IntegrationTests/ # Интеграционные тесты
├── docker/                      # Docker конфигурации
│   ├── seq/                     # Конфигурация Seq
│   └── minio/                   # Конфигурация MinIO
├── docker-compose.yml          # Docker Compose (все сервисы)
└── README.md
