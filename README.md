# Prueba DGII

Este repositorio contiene una solución completa para la gestión de contribuyentes y comprobantes fiscales.

---

## Requisitos Previos

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- (Opcional) Visual Studio 2022 o VS Code

---


## Cómo ejecutar el proyecto

### 1. Backend (ASP.NET Core)

1. Abre una terminal y navega al directorio `backend`:
    ```sh
    cd backend
    ```

2. Restaura los paquetes NuGet:
    ```sh
    dotnet restore
    ```

3. Ejecuta la API:
    ```sh
    dotnet run
    ```
    - La API estará disponible en: `http://localhost:5000/api`
    - Documentación Swagger: `http://localhost:5000/swagger`

---

### 2. Frontend (React + Vite)

1. Abre otra terminal y navega al directorio `frontend`:
    ```sh
    cd frontend
    ```

2. Instala las dependencias:
    ```sh
    npm install
    ```

3. Ejecuta la aplicación:
    ```sh
    npm run dev
    ```
    - La web estará disponible en: `http://localhost:5173`

---

## Conexión entre Frontend y Backend

- El frontend realiza peticiones al backend en `http://localhost:5000/api`.
- El backend tiene CORS habilitado para permitir peticiones desde el frontend.
- Ambos servidores deben estar corriendo al mismo tiempo.



Ejecuta los tests con:
