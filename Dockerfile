#FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["VehiclesApp/VehiclesApp.csproj", "VehiclesApp/"]
#RUN dotnet restore "VehiclesApp/VehiclesApp.csproj"
#COPY . .
#WORKDIR "/src/VehiclesApp"
#RUN dotnet build "VehiclesApp.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "VehiclesApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "VehiclesApp.dll"]

# STEP 1 - Build Angular
#FROM node:18 AS angular-build
#WORKDIR /app
#COPY ClientApp ./ClientApp
#WORKDIR /app/ClientApp
#RUN npm install
#RUN npm run build --prod
#
## STEP 2 - Build .NET API and include Angular dist
#FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
#WORKDIR /src
#COPY  ["VehiclesApp/VehiclesApp.csproj", "VehiclesApp/"]
#RUN dotnet restore "VehiclesApp/VehiclesApp.csproj"
#COPY . .
#WORKDIR /src/MyApi
#RUN dotnet publish -c Release -o /app/publish
#
## STEP 3 - Final runtime image
#FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
#WORKDIR /app
#COPY --from=build /app/publish .
## Copy Angular dist into wwwroot
#COPY --from=angular-build /app/ClientApp/dist/ ./wwwroot/
#ENTRYPOINT ["dotnet", "VehiclesApp.dll"]


# ==============================
# 1. Build Angular
# ==============================
FROM node:20 AS angular-build
WORKDIR /app/Client/angular

# Copy Angular project files
COPY Client/angular/package*.json ./
RUN npm install

COPY  Client/angular/ ./
RUN npm run build --prod

# ==============================
# 2. Build .NET Backend
# ==============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS dotnet-build

WORKDIR /src

# Copy solution and projects
COPY VehiclesApp.sln ./
COPY VehiclesApp/*.csproj ./VehiclesApp/
COPY Entity/*.csproj ./Entity/

# Restore NuGet packages
RUN dotnet restore

# Copy all source code
COPY VehiclesApp/ ./VehiclesApp/
COPY Entity/ ./Entity/

# Publish the solution (creates /app/publish)

RUN dotnet publish VehiclesApp/VehiclesApp.csproj -c Release -o /app/publish

# ==============================
# 3. Final Runtime Image
# ==============================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published .NET app
COPY --from=dotnet-build /app/publish ./

# Copy Angular build into wwwroot

COPY --from=angular-build /app/Client/angular/dist/angular/browser ./wwwroot/

# Expose port
EXPOSE 8080
ENV DOTNET_URLS=http://0.0.0.0:8080
ENV API_URL=http://localhost:5000
ENTRYPOINT ["sh", "-c", "sed -i 's|http://localhost:5000|'$API_URL'|g' ./wwwroot/config.js && dotnet VehiclesApp.dll"]





