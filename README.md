# Company.WPF - Управление компанией

WPF приложение для управления сотрудниками, контрагентами и заказами.

## Требования

- Windows 10/11
- Docker Desktop (для запуска базы данных)
- .NET 8.0

## Быстрый старт

### 1. Запуск базы данных через Docker

Создайте и запустите контейнер MySQL:

```bash
docker run --name company-mysql \
  -e MYSQL_ROOT_PASSWORD=123456 \
  -e MYSQL_DATABASE=companydb \
  -p 3306:3306 \
  -v mysql-data:/var/lib/mysql \
  -d mysql:8
