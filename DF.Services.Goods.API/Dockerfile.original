#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DF.Services.Goods.API/DF.Services.Goods.API.csproj", "DF.Services.Goods.API/"]
RUN dotnet restore "DF.Services.Goods.API/DF.Services.Goods.API.csproj"
COPY . .
WORKDIR "/src/DF.Services.Goods.API"
RUN dotnet build "DF.Services.Goods.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DF.Services.Goods.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DF.Services.Goods.API.dll"]