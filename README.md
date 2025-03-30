# Sales Data Prediction

Este monorepo contiene tanto el backend como el frontend de la aplicación **Sales Data Prediction**. A continuación, se detallan los pasos para instalar y ejecutar cada parte del sistema.

## 📋 Requisitos Previos

Asegúrate de tener instalado lo siguiente antes de continuar:

- **Docker** y **Docker Compose**
- **Node.js** (versión recomendada: 18.x o superior)
- **Angular CLI** (para el frontend)
- **.NET 8** (para el backend)

## 🚀 Instalación y Ejecución

### Ejecución Completa con Docker Compose

Para ejecutar todo el proyecto (backend y frontend) con Docker Compose, simplemente corre el siguiente comando en la raíz del repositorio:

```bash
docker-compose up --build
```

Esto iniciará todos los servicios necesarios.

Si necesitas reconstruir las imágenes, usa:

```bash
docker-compose up --build --force-recreate
```

---

### Backend

#### Variables de Entorno
Las configuraciones del backend están definidas en el archivo `.env`, el cual debe colocarse en el directorio raíz del backend antes de ejecutar el servicio.

#### Construcción y Ejecución Manual (Sin Docker)

Si prefieres ejecutar el backend sin Docker, sigue estos pasos:

1. Ve al directorio del backend y ejecuta:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

2. El backend estará disponible en `http://localhost:8080/api`, aunque este puerto puede variar dependiendo del entorno.

#### Ejecución de Pruebas Unitarias
Para ejecutar las pruebas unitarias en el backend, usa el siguiente comando dentro del directorio raíz del backend:

```bash
dotnet test
```

Este comando ejecutará todas las pruebas unitarias y mostrará los resultados en la terminal.

---

### Frontend

#### Configuración de la URL de la API

El frontend utiliza archivos de configuración `environment.ts` y `environment.prod.ts`. Si la URL de la API cambia, actualiza el valor de `apiUrl` en estos archivos:

```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:8080'
};
```

#### Instalación de Dependencias

Desde el directorio del frontend, ejecuta:

```bash
yarn install
```

#### Ejecución en Desarrollo

Para iniciar el servidor de desarrollo:

```bash
yarn start
# o
ng serve
```

La aplicación estará disponible en `http://localhost:4200/`.

#### Construcción para Producción

Para construir el frontend:

```bash
yarn build
# o
ng build --prod
```

Los archivos se generarán en `dist/` y estarán listos para su despliegue.

---

### App Chart

Para ejecutar la aplicación App Chart, simplemente abre el archivo `index.html` en tu navegador.

---

## 📂 Estructura del Proyecto

El monorepo sigue una organización modular para facilitar el mantenimiento y la escalabilidad:

```
/ sales-date-prediction
│── sales-date-prediction-backend/   # Código fuente del backend en .NET 8
│   ├── src/
│   │   ├── SalesDatePrediction.Api/           # API principal
│   │   │   ├── Controllers/                   # Controladores API
│   │   │   ├── Extensions/                    # Métodos de extensión
│   │   │   ├── Middlewares/                   # Middlewares personalizados
│   │   │   ├── Program.cs                     # Archivo de entrada
│   │   │   ├── appsettings.json                # Configuración de la API
│   │   ├── SalesDatePrediction.Application/   # Lógica de negocio
│   │   ├── SalesDatePrediction.Domain/        # Modelos y contratos
│   │   ├── SalesDatePrediction.Infrastructure/# Acceso a datos
│   ├── tests/                                # Pruebas unitarias
│
│── sales-date-prediction-frontend/  # Código fuente del frontend en Angular
│   ├── src/
│   │   ├── app/                      # Componentes y módulos de la app
│   │   ├── environments/             # Configuraciones de entorno
│   ├── dist/                         # Archivos compilados
│   ├── package.json                  # Dependencias del frontend
│
│── App-chart/                        # Visualización de datos en HTML
│   ├── index.html                    # Archivo HTML principal
│
│── docker-compose.yml                 # Configuración para ejecutar con Docker
│── README.md                           # Documentación del proyecto
```

Esta estructura permite un desarrollo organizado, separando claramente el backend, el frontend y los archivos de configuración.

---

## 📄 Notas Adicionales

- **El backend utiliza un archivo `.env`** para configurar variables sensibles.
- **Si se ejecuta en un entorno diferente**, actualizar `environment.ts` y `.env` con las URLs correctas.

---

## 📌 Repositorio

El código fuente está disponible en [GitHub](https://github.com/Galaoox/sales-date-prediction).
