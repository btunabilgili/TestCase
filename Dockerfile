#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Presentation/WebAPI/TestCase.WebAPI/TestCase.WebAPI.csproj", "src/Presentation/WebAPI/TestCase.WebAPI/"]
COPY ["src/Infrastructure/TestCase.Infrastructure/TestCase.Infrastructure.csproj", "src/Infrastructure/TestCase.Infrastructure/"]
COPY ["src/Core/TestCase.Application/TestCase.Application.csproj", "src/Core/TestCase.Application/"]
COPY ["src/Core/TestCase.Domain/TestCase.Domain.csproj", "src/Core/TestCase.Domain/"]
RUN dotnet restore "src/Presentation/WebAPI/TestCase.WebAPI/TestCase.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Presentation/WebAPI/TestCase.WebAPI"
RUN dotnet build "TestCase.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestCase.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestCase.WebAPI.dll"]