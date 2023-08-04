#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /base-netcore
COPY . .
RUN dotnet restore "./Base.Application/Base.Application.csproj" --disable-parallel
RUN dotnet publish "./Base.Application/Base.Application.csproj" -c release -o /app --no-restore
# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 5000
RUN ls /app
ENTRYPOINT [ "dotnet", "Base.Application.dll", "--urls", "http://0.0.0.0:5000" ]