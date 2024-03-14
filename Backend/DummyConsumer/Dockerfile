FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DummyConsumer/DummyConsumer.csproj", "DummyConsumer/"]
RUN dotnet restore "DummyConsumer/DummyConsumer.csproj"
COPY . .
WORKDIR "/src/DummyConsumer"
RUN dotnet build "DummyConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DummyConsumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 3000
EXPOSE 80
ENTRYPOINT ["dotnet", "DummyConsumer.dll"]
