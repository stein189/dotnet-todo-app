# Running the project locally

1. Make sure the infrastructure is running:
```
docker-compose -p codehub -f docker-compose.infrastructure.yml up -d
```

2. Run the project:
```
docker-compose -p codehub run my-app dotnet run --project src/Projection/Projection.csproj
```
