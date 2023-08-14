#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Base.Application/Base.Application.csproj", "Base.Application/"]
COPY ["Base.Business/Base.Business.csproj", "Base.Business/"]
COPY ["Base.Services/Base.Services.csproj", "Base.Services/"]
COPY ["Base.Utils/Base.Utils.csproj", "Base.Utils/"]
COPY ["Base.Core/Base.Core.csproj", "Base.Core/"]
RUN dotnet restore "Base.Application/Base.Application.csproj"
COPY . .
WORKDIR "/src/Base.Application"
RUN dotnet build "Base.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Base.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Base.Application.dll"]