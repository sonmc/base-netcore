 
#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /Src
COPY . .
RUN dotnet restore "./BaseNetCore.csproj" --disable-parallel
RUN dotnet publish "./BaseNetCore.csproj" -c release -o /app --no-restore
# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 5000
RUN ls /app
ENTRYPOINT [ "dotnet", "BaseNetCore.dll", "--urls", "http://0.0.0.0:5000" ]