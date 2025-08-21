Vehicles App

A simple web app that lets users pick a car make and manufacture year, then see available vehicle types and models using the provided public APIs.

🧰 Tech Stack
-Backend: ASP.NET Core (Web API)
-Frontend: Angular (SPA)
-Container: Docker (Linux containers)
-Package manager: npm

To run the App locally pleas follow these steps:
# Build (from repo root)
docker build -t vehicles-app:local .

# Run (map container 8080 → host 8080)
docker run --rm -p 8080:8080 vehicles-app:local
