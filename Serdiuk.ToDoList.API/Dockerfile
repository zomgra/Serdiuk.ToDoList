#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Serdiuk.ToDoList.API/Serdiuk.ToDoList.API.csproj", "Serdiuk.ToDoList.API/"]
RUN dotnet restore "Serdiuk.ToDoList.API/Serdiuk.ToDoList.API.csproj"
COPY . .
WORKDIR "/src/Serdiuk.ToDoList.API"
RUN dotnet build "Serdiuk.ToDoList.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Serdiuk.ToDoList.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Serdiuk.ToDoList.API.dll"]