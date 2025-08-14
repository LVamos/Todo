# build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# copy project files first to optimize restore layer
COPY Todo.App/Todo.App.csproj Todo.App/
COPY Todo.Api/Todo.Api.csproj Todo.Api/
COPY Todo.Constants/Todo.Constants.csproj Todo.Constants/
COPY Todo.ModelViews/Todo.Contracts.csproj Todo.ModelViews/
COPY Todo.Dal/Todo.Dal.csproj Todo.Dal/
COPY Todo.Entities/Todo.Entities.csproj Todo.Entities/
COPY Todo.ServerConfigurations/Todo.ServerConfigurations.csproj Todo.ServerConfigurations/
COPY Todo.Services.Abstraction/Todo.Services.Abstraction.csproj Todo.Services.Abstraction/
COPY Todo.Services/Todo.Services.csproj Todo.Services/

RUN dotnet restore Todo.App/Todo.App.csproj

# copy the rest and publish
COPY . .
RUN dotnet publish Todo.App/Todo.App.csproj -c Release -o /app/out

# runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Todo.App.dll"]