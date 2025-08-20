# PetFamily 🐾  
# Примечание : этот проект в настоящее время находится в стадии разработки.

PetFamily - это платформа для поиска новых хозяев бездомным животным. Система позволяет волонтерам публиковать информацию о животных, нуждающихся в доме, а потенциальные хозяева - смогут удобно просматривать эти объявления и подавать заявки на животных, которых они хотели бы взять в приют

Проект построен по принципам Clean Architecture и включает:
+ Domain Layer - бизнес-логика и сущности
+ Application Layer - use cases и CQRS
+ Infrastructure Layer - работа с БД, внешние сервисы
+ API Layer - Web API контроллеры

# 🛠️ Технологический стек  
Backend
+ .NET 8 - основная платформа
+ ASP.NET Core Web API - REST API
+ Entity Framework Core 8 - ORM
+ PostgreSQL - основная база данных
+ Serilog - структурированное логирование
+ Seq - система просмотра и анализа логов
+ FluentValidation - валидация входных данных
+ Swagger/OpenAPI - документация API
+ MinIO - S3-совместимое хранилище файлов

# Инфраструктура
+ Docker - контейнеризация
+ Docker Compose - оркестрация
+ PostgreSQL - основная СУБД

# Клонирование репозитория  
```git clone https://github.com/aleksey-sosnovcev/PetFamily.git  
cd PetFamily```

# 📁 Структура проекта
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
├── docker-compose.yml          # Docker Compose (все сервисы)  
└── README.md  
