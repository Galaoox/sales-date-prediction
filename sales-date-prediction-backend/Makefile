.PHONY: build run test clean docker-build docker-run help

# Project Configuration
SOLUTION_DIR = .
API_PROJECT = src/SalesDatePrediction.Api
SOLUTION_FILE = SalesDatePrediction.sln

# Docker Configuration
DOCKER_IMAGE = sales-date-prediction-api
DOCKER_TAG = latest

# .NET Commands
build:
	@echo "Compilando la solución..."
	dotnet build $(SOLUTION_FILE)

run:
	@echo "Ejecutando proyecto API..."
	dotnet run --project $(API_PROJECT)

test:
	@echo "Ejecutando pruebas de toda la solución..."
	dotnet test $(SOLUTION_FILE)

clean:
	@echo "Limpiando solución..."
	dotnet clean $(SOLUTION_FILE)
	find . -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +

# Docker Commands
docker-build:
	@echo "Construyendo imagen Docker..."
	docker build -t $(DOCKER_IMAGE):$(DOCKER_TAG) -f Dockerfile .

docker-run:
	@echo "Ejecutando contenedor Docker..."
	docker run -p 7000:8080 $(DOCKER_IMAGE):$(DOCKER_TAG)

# Additional Utilities
restore:
	@echo "Restaurando paquetes NuGet..."
	dotnet restore $(SOLUTION_FILE)

publish:
	@echo "Publicando proyecto API..."
	dotnet publish $(API_PROJECT) -c Release -o ./publish

# Help Command
help:
	@echo "Comandos disponibles:"
	@echo "  make build        - Compila toda la solución"
	@echo "  make run          - Ejecuta el proyecto API"
	@echo "  make test         - Ejecuta todas las pruebas"
	@echo "  make clean        - Limpia archivos de compilación"
	@echo "  make docker-build - Construye imagen Docker"
	@echo "  make docker-run   - Ejecuta contenedor Docker"
	@echo "  make restore      - Restaura paquetes NuGet"
	@echo "  make publish      - Publica proyecto API"