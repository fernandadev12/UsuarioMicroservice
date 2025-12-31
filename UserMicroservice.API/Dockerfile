FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY *.sln .
COPY UserMicroservice.API/*.csproj ./UserMicroservice.API/
COPY UserMicroservice.Application/*.csproj ./UserMicroservice.Application/
COPY UserMicroservice.Domain/*.csproj ./UserMicroservice.Domain/
COPY UserMicroservice.Infra/*.csproj ./UserMicroservice.Infra/
COPY UserMicroservice.Test/*.csproj ./UserMicroservice.Test/ 

COPY UsuarioMicroservice.sln .
RUN dotnet restore "UsuarioMicroservice.sln" --runtime linux-musl-x64

COPY . .

WORKDIR /src/UserMicroservice.API
RUN dotnet publish "UserMicroservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "UserMicroservice.API.dll"]

# docker build --no-cache -t microserviceusuario:latest -f UserMicroservice.API/Dockerfile .
# docker run -d -p 5000:8080 --name user-microservice microserviceusuario:latest