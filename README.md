# PredictiveMaintenance

Proyecto de mantenimiento predictivo desarrollado en **ASP.NET Core** y **Entity Framework Core**.  
Permite gestionar máquinas, registrar datos de sensores y generar alertas de mantenimiento para anticipar fallos.

---

## 📌 Características
- CRUD completo de **Máquinas** (`Machine`).
- Registro y consulta de **datos de sensores** (`SensorData`).
- Generación de **alertas de mantenimiento** (`MaintenanceAlert`).
- Documentación automática con **Swagger/OpenAPI**.
- Arquitectura por capas: Domain, Application, Infrastructure, Api, Worker, Simulator.

---

## 🏗️ Arquitectura
El proyecto sigue una arquitectura limpia y organizada:

- **Domain** → Entidades principales (`Machine`, `SensorData`, `MaintenanceAlert`).
- **Application** → DTOs, interfaces y lógica de negocio.
- **Infrastructure** → Persistencia con EF Core y configuraciones.
- **Api** → Endpoints REST con ASP.NET Core.
- **Worker/Simulator** → Procesos de simulación y tareas en segundo plano.

---

## ⚙️ Instalación
1. Clonar el repositorio:
   ```bash
   git clone https://github.com/usuario/PredictiveMaintenance.git
2. Restaurar dependencias:
   dotnet restore
- Aplicar migraciones:
dotnet ef database update


3. Ejecutar el proyecto:
   dotnet run --project PredictiveMaintenance.Api


🔗 Endpoints principales
- POST /api/Machines → Crear máquina
- GET /api/Machines/{id} → Obtener máquina por ID
- GET /api/Machines → Listar todas las máquinas
- PUT /api/Machines/{id} → Actualizar máquina
- DELETE /api/Machines/{id} → Eliminar máquina
- POST /api/SensorData → Registrar lectura de sensor
- GET /api/SensorData/machine/{id} → Historial de lecturas

🛠️ Tecnologías
- ASP.NET Core 10
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI
- GitHub para control de versiones

🔒 Seguridad
- .gitignore configurado para evitar subir archivos sensibles (bin/, obj/, .vs/, appsettings.Development.json).
- Buenas prácticas de commits y ramas (main como rama principal).
