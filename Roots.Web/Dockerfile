#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Roots.Web/Roots.Web.csproj", "Roots.Web/"]
COPY ["Roots.Business/Roots.Business.csproj", "Roots.Business/"]
COPY ["Roots.Domain/Roots.Domain.csproj", "Roots.Domain/"]
COPY ["Roots.Data/Roots.Data.csproj", "Roots.Data/"]
RUN dotnet restore "Roots.Web/Roots.Web.csproj"
COPY . .
WORKDIR "/src/Roots.Web"
RUN dotnet build "Roots.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Roots.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/Roots.Web/Roots.Web.xml .
ENTRYPOINT ["dotnet", "Roots.Web.dll"]