# STUB: Superficie de práctica - el estudiante implementa el Dockerfile completo
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["AccountManagement.Api/AccountManagement.Api.csproj", "AccountManagement.Api/"]
COPY ["AccountManagement.Application/AccountManagement.Application.csproj", "AccountManagement.Application/"]
COPY ["AccountManagement.Domain/AccountManagement.Domain.csproj", "AccountManagement.Domain/"]
COPY ["AccountManagement.Infrastructure/AccountManagement.Infrastructure.csproj", "AccountManagement.Infrastructure/"]
COPY ["AccountManagement.Common/AccountManagement.Common.csproj", "AccountManagement.Common/"]
RUN dotnet restore "AccountManagement.Api/AccountManagement.Api.csproj"
COPY . .
WORKDIR "/src/AccountManagement.Api"
RUN dotnet build "AccountManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountManagement.Api.dll"]