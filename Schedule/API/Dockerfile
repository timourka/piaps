# ==== Этап 1: build ====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Устанавливаем рабочую директорию
WORKDIR /src

# Копируем sln и csproj-файлы (для кэширования restore)
COPY *.sln ./
COPY Models/Models.csproj Models/
COPY Repositories/Repositories.csproj Repositories/
COPY API/API.csproj API/

# Восстанавливаем зависимости
RUN dotnet restore

# Копируем остальной исходный код
COPY . .

# Публикуем (Release)
RUN dotnet publish API/API.csproj -c Release -o /app/publish

# ==== Этап 2: runtime ====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Устанавливаем рабочую директорию
WORKDIR /app

# Копируем опубликованные файлы
COPY --from=build /app/publish .

# Открываем порт (по необходимости, по умолчанию ASP.NET использует 80)
EXPOSE 80

# Команда запуска
ENTRYPOINT ["dotnet", "API.dll"]
