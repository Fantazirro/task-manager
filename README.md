# task-manager
Это веб-приложение, позволяющее эффективно управлять своими задачами

<h3 align="center">Главная страница</h3>
<p align="center">
<img src="https://i.imgur.com/W9LiOsz.jpeg" alt="Task Manager" height="400" style="align:center;"/>
</p>

## Настройка сервиса
Настроить сервис можно с помощью следующих файлов конфигурации:
* **appsettings.json** - конфигурация и настройка API
* **compose.yml** - конфигурация всей системы

## Запуск системы
Для запуска системы вам нужны следующие инструменты: Docker и Docker Compose  

Команда для запуска:
```bash
sudo docker compose up --build
```

## Стек технологий
* **Web API**: ASP.NET Core
* **База данных**: PostgreSQL
* **Кэш-сервер**: Memcached
* **Контейнеризация**: Docker, Docker Compose

## Библиотеки и фреймворки
* **Entity Framework Core** - для подключения к базе данных
* **Fluent Validation** - для валидации входных данных
* **AutoMapper** - для маппинга моделей
* **Swagger** - для документирования API
* **FluentEmail.Smtp** - для связи с SMTP-сервером
* **Microsoft.Extensions.Caching.StackExchangeRedis** - для подключения к кэш-серверу
* **BCrypt.Net-Next** - для хеширования паролей пользователей

## Архитектурные подходы
* **MVC**
* **Clean Architecture**
* **Распределённая система**
