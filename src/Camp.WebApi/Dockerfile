#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT Production

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Camp.WebApi/Camp.WebApi.csproj", "src/Camp.WebApi/"]
COPY ["src/Camp.BusinessLogicLayer/Camp.BusinessLogicLayer.csproj", "src/Camp.BusinessLogicLayer/"]
COPY ["src/Camp.DataAccess/Camp.DataAccess.csproj", "src/Camp.DataAccess/"]
COPY ["src/Camp.Common/Camp.Common.csproj", "src/Camp.Common/"]
COPY ["src/Camp.Security/Camp.Security.csproj", "src/Camp.Security/"]
RUN dotnet restore "src/Camp.WebApi/Camp.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Camp.WebApi"
RUN dotnet build "Camp.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Camp.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Camp.WebApi.dll"]
