import { useEffect, useState } from 'react';
import axios from 'axios';

interface Health {
  api: string;
  database: string;
  rabbitmq: string;
}

const apiUrl = import.meta.env.VITE_API_URL ?? 'http://localhost:5080';

export function App() {
  const [health, setHealth] = useState<Health | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    axios
      .get<Health>(`${apiUrl}/health`)
      .then((res) => setHealth(res.data))
      .catch((err) => setError(err.message));
  }, []);

  const rows: [string, string | undefined][] = [
    ['API', health?.api],
    ['Base de datos (SQLite)', health?.database],
    ['RabbitMQ', health?.rabbitmq],
  ];

  return (
    <main>
      <h1>OrderHub · Verificación de entorno</h1>
      {error && <p className="error">No se pudo contactar la API en {apiUrl}: {error}</p>}
      {!error && !health && <p>Consultando {apiUrl}/health…</p>}
      {health && (
        <ul>
          {rows.map(([name, status]) => (
            <li key={name} className={status === 'ok' ? 'ok' : 'error'}>
              {status === 'ok' ? '✅' : '❌'} {name}: {status === 'ok' ? 'OK' : status}
            </li>
          ))}
        </ul>
      )}
    </main>
  );
}
