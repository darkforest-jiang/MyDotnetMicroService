#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DF.Services.Order.API/DF.Services.Order.API.csproj", "DF.Services.Order.API/"]
RUN dotnet restore "DF.Services.Order.API/DF.Services.Order.API.csproj"
COPY . .
WORKDIR "/src/DF.Services.Order.API"
RUN dotnet build "DF.Services.Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DF.Services.Order.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DF.Services.Order.API.dll"]