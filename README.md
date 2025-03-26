# Módulo 4: `Semaphore` en C#

## 🔒 ¿Qué es un `Semaphore`?

`Semaphore` es un mecanismo de sincronización del espacio de nombres `System.Threading` que **controla el número de hilos (o procesos) que pueden acceder simultáneamente a un recurso**.

A diferencia de `lock` o `Mutex`, **permite múltiples accesos concurrentes**, y a diferencia de `SemaphoreSlim`, puede ser usado para sincronización **entre procesos**.

---

## 🏗️ Escenario práctico: **Límite de conexiones a un recurso compartido**

Supongamos que tenemos 3 conexiones disponibles a una base de datos o a una impresora compartida en red. `Semaphore` puede permitir que solo 3 hilos o procesos usen ese recurso simultáneamente, mientras que los demás esperan su turno.

### Ejemplo simple de uso

```csharp
private static Semaphore _semaforo = new(initialCount: 3, maximumCount: 3);

public static void AccederRecurso(string nombre)
{
    _semaforo.WaitOne();
    try
    {
        Console.WriteLine($"{nombre} accediendo...");
        Thread.Sleep(1000);
    }
    finally
    {
        Console.WriteLine($"{nombre} saliendo.");
        _semaforo.Release();
    }
}
```

---

## 🔄 ¿Por qué usar `Semaphore` en lugar de `SemaphoreSlim`?

| Característica                  | `Semaphore` ✅ | `SemaphoreSlim` ❌ |
|--------------------------------|----------------|--------------------|
| Sincronización entre procesos  | ✅ Sí           | ❌ No              |
| Uso compartido por nombre      | ✅ Sí           | ❌ No              |
| Compatible con otras apps/sistemas | ✅ Sí       | ❌ No              |

🔍 `Semaphore` es ideal cuando necesitás **controlar el acceso desde múltiples procesos**, como en servicios del sistema operativo o ejecutables distintos.

---

## 💡 Entonces, ¿por qué existe `SemaphoreSlim`?

`SemaphoreSlim` fue introducido en .NET para:
- Optimizar el rendimiento **dentro de una única aplicación/proceso**.
- Habilitar sincronización **asíncrona** (`WaitAsync`, `Release`).
- Ser más liviano en recursos que `Semaphore`.

✅ Usá `SemaphoreSlim` para código moderno, asíncrono y **dentro de un solo proceso**.  
✅ Usá `Semaphore` si necesitás interoperabilidad entre procesos o si querés nombrar semáforos a nivel del sistema.

---

## 🧼 Buenas prácticas con `Semaphore`

| Regla | Motivo |
|-------|--------|
| ✅ Siempre usá `try/finally` para llamar `Release()` | Evita fugas o bloqueos |
| ✅ Documentá claramente el uso del semáforo global | Otros procesos pueden verse afectados |
| ❌ No mezcles `Semaphore` con `SemaphoreSlim` en el mismo contexto compartido | Tienen propósitos distintos |

---

✅ Este módulo cubre `Semaphore`, ideal para coordinación avanzada, incluso entre procesos.  
🧵 Para sincronización dentro de tu app moderna, preferí `SemaphoreSlim` (Módulo 4).