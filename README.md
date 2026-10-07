# OrderHub · Verificación de entorno

¡Gracias por participar en nuestro proceso! La prueba técnica será una **sesión en vivo de ~90 minutos** con pantalla compartida, sobre una aplicación existente.

Este repositorio **no es la prueba**: solo sirve para verificar que tu máquina tiene todo lo necesario, y así no perder tiempo de la sesión configurando el entorno. Además, deja descargados los paquetes y las imágenes que usaremos ese día.

## Stack de la prueba

**.NET 8 · ASP.NET Core Web API · EF Core (SQLite) · JWT · RabbitMQ · React + TypeScript (Vite) · Docker**

## Requisitos

| Herramienta | Versión |
|---|---|
| .NET SDK | 8.0 (o superior, con el runtime de .NET 8 instalado) |
| Node.js | 20 LTS o superior |
| Docker | Docker Desktop, Colima, Rancher Desktop o equivalente, con `docker compose` |
| Git | cualquiera reciente |
| IDE | el que prefieras: Visual Studio, Rider o VS Code |

Puertos libres: `5080` (API), `5173` (web), `5672` y `15672` (RabbitMQ).

## Verificación

Ejecuta estos pasos **desde la raíz del repo**:

```bash
# 1. RabbitMQ
docker compose up -d
#    → abre http://localhost:15672  (usuario: guest / contraseña: guest)

# 2. API
dotnet run --project src/OrderHub.Api
#    → abre http://localhost:5080/swagger y ejecuta GET /health

# 3. Worker (en otra terminal)
dotnet run --project src/OrderHub.Worker
#    → en la consola debe aparecer "Worker OK: conectado a RabbitMQ."

# 4. Front (en otra terminal)
cd web
npm ci
npm run dev
#    → abre http://localhost:5173: los tres checks deben estar en ✅

# 5. Tests (en otra terminal, desde la raíz del repo)
dotnet test
```

**Opcional:** verifica que también puedes levantar todo en contenedores. Así quedan descargadas las imágenes base. Antes, detén los procesos de los pasos 2 a 4 (Ctrl+C), porque usan los mismos puertos.

```bash
docker compose --profile full up --build
```

**Al terminar, apaga todo** para liberar los puertos para el día de la sesión:

```bash
docker compose --profile full down
```

Cuando termines, **responde el correo con una captura de la pantalla del paso 4**, o con el error que encontraste. Te ayudamos a resolverlo antes de la sesión.

## Durante la sesión

- Compartirás tu **pantalla completa** (no solo una ventana).
- Al inicio de la llamada te daremos acceso al repositorio de la prueba.
- Puedes usar Google, la documentación oficial y Stack Overflow.
- **No está permitido usar asistentes de IA** (GitHub Copilot, Cursor, ChatGPT, Claude, etc.). Desactiva sus extensiones del IDE antes de la sesión.
- Te pediremos que **pienses en voz alta**. Nos interesa más tu razonamiento que terminar todo.

## Problemas comunes

| Síntoma | Solución |
|---|---|
| `address already in use` en el puerto 5080 o 5173 | Cierra el proceso que lo ocupa, o avísanos |
| La API no conecta con RabbitMQ | Revisa que el contenedor esté `healthy`: `docker compose ps` |
| `npm ci` falla | Verifica tu versión de Node (`node -v` ≥ 20) |
| `dotnet run` dice que falta el framework 8.0 | Instala el runtime de .NET 8 aunque tengas un SDK más nuevo |
