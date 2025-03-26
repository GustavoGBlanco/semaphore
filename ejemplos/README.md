# Ejemplos prácticos y profesionales de `Semaphore` en C#

Este documento presenta 10 ejemplos realistas y técnicamente justificados del uso de `Semaphore` (clásico, del espacio de nombres `System.Threading`) en C#. Cada ejemplo está diseñado con hilos (`Thread`) y enfocado en escenarios donde se necesita controlar el acceso a recursos compartidos desde múltiples hilos (o incluso múltiples procesos).

---

## 🧪 Ejemplo 1: Acceso controlado a recurso limitado

```csharp
private static Semaphore _semaforo = new(initialCount: 2, maximumCount: 2);

public static void AccederRecurso(string nombre)
{
    _semaforo.WaitOne();
    try
    {
        Console.WriteLine($"{nombre} accediendo...");
        Thread.Sleep(500);
    }
    finally
    {
        Console.WriteLine($"{nombre} saliendo.");
        _semaforo.Release();
    }
}
```

🔍 Simula acceso a un recurso que puede ser usado por dos hilos simultáneamente.

✅ **¿Por qué `Semaphore`?**  
Permite concurrencia parcial. `lock` no sirve si querés permitir más de un acceso al mismo tiempo.

📊 **Comparación:**
- 🔐 `lock`: permite solo un hilo.
- 🧵 `SemaphoreSlim`: más liviano, pero `Semaphore` puede compartirse entre procesos.

---

## 🧪 Ejemplo 2: Limitar acceso concurrente a archivo compartido

```csharp
private static Semaphore _archivo = new(1, 1);

public static void EscribirArchivo(string hilo)
{
    _archivo.WaitOne();
    try
    {
        Console.WriteLine($"{hilo} escribiendo archivo...");
        Thread.Sleep(400);
    }
    finally
    {
        Console.WriteLine($"{hilo} terminó de escribir.");
        _archivo.Release();
    }
}
```

🔍 Solo un hilo escribe al archivo al mismo tiempo.

✅ **¿Por qué `Semaphore`?**  
Permite reemplazar `lock` si se desea interoperabilidad con otros procesos.

📊 **Comparación:**
- 🔁 `Mutex`: también válido, pero más costoso.
- 🔐 `lock`: solo dentro del mismo proceso.

---

## 🧪 Ejemplo 3: Control de acceso a zona crítica compartida

```csharp
private static Semaphore _zonaCritica = new(2, 2);

public static void IngresarZona(string usuario)
{
    _zonaCritica.WaitOne();
    try
    {
        Console.WriteLine($"{usuario} en zona crítica.");
        Thread.Sleep(300);
    }
    finally
    {
        Console.WriteLine($"{usuario} saliendo.");
        _zonaCritica.Release();
    }
}
```

🔍 Simula una sección crítica a la que solo se puede acceder de a dos.

✅ **¿Por qué `Semaphore`?**  
`lock` y `Monitor` bloquean a todos los hilos salvo uno.

📊 **Comparación:**
- 🔁 `SemaphoreSlim`: opción más moderna, pero no apta para múltiples procesos.

---

## 🧪 Ejemplo 4: Acceso compartido a impresora (solo 1 al mismo tiempo)

```csharp
private static Semaphore _impresora = new(1, 1);

public static void Imprimir(string documento)
{
    _impresora.WaitOne();
    try
    {
        Console.WriteLine($"Imprimiendo {documento}...");
        Thread.Sleep(600);
    }
    finally
    {
        Console.WriteLine($"{documento} impreso.");
        _impresora.Release();
    }
}
```

🔍 Se simula acceso exclusivo a una impresora.

✅ **¿Por qué `Semaphore`?**  
Ofrece sincronización entre hilos o procesos si fuera necesario.

📊 **Comparación:**
- 🔁 `SemaphoreSlim`: solo dentro del proceso.
- 🔐 `lock`: más rápido, pero sin interoperabilidad.

---

## 🧪 Ejemplo 5: Control de conexiones simultáneas en servidor

```csharp
private static Semaphore _conexiones = new(3, 3);

public static void ManejarConexion(string cliente)
{
    _conexiones.WaitOne();
    try
    {
        Console.WriteLine($"{cliente} conectado.");
        Thread.Sleep(500);
    }
    finally
    {
        Console.WriteLine($"{cliente} desconectado.");
        _conexiones.Release();
    }
}
```

🔍 Permite un máximo de 3 clientes conectados a la vez.

✅ **¿Por qué `Semaphore`?**  
Ideal para limitar conexiones simultáneas a un recurso crítico.

📊 **Comparación:**
- 🔁 `SemaphoreSlim`: útil si es intra-proceso.
- 🔐 `lock`: restringe a uno solo.

---

## 🧪 Ejemplo 6: Control de turnos en estaciones de trabajo

```csharp
private static Semaphore _estacion = new(1, 1);

public static void UsarEstacion(string operario)
{
    _estacion.WaitOne();
    try
    {
        Console.WriteLine($"{operario} usando estación de trabajo.");
        Thread.Sleep(400);
    }
    finally
    {
        Console.WriteLine($"{operario} liberó la estación.");
        _estacion.Release();
    }
}
```

🔍 Controla el acceso uno a uno entre operarios.

✅ **¿Por qué `Semaphore`?**  
Compatible con otros procesos si se compartiera, y mantiene lógica simple.

📊 **Comparación:**
- 🔐 `lock`: sólo intra-proceso.
- 🔁 `SemaphoreSlim`: no compartible entre procesos.

---

## 🧪 Ejemplo 7: Procesamiento por lotes (grupo de 3 hilos)

```csharp
private static Semaphore _lote = new(3, 3);

public static void ProcesarLote(string tarea)
{
    _lote.WaitOne();
    try
    {
        Console.WriteLine($"{tarea} procesando...");
        Thread.Sleep(300);
    }
    finally
    {
        Console.WriteLine($"{tarea} finalizada.");
        _lote.Release();
    }
}
```

🔍 Permite que hasta 3 tareas se procesen a la vez.

✅ **¿Por qué `Semaphore`?**  
Es una forma sencilla de agrupar tareas concurrentes sin esperar a fases completas.

📊 **Comparación:**
- 🔄 `Barrier`: obliga a sincronización entre fases.
- 🔁 `SemaphoreSlim`: alternativa moderna, menos potente entre procesos.

---

## 🧪 Ejemplo 8: Límite de acceso concurrente a base de datos

```csharp
private static Semaphore _conexionBD = new(2, 2);

public static void ConsultarBD(string nombre)
{
    _conexionBD.WaitOne();
    try
    {
        Console.WriteLine($"{nombre} consultando base de datos...");
        Thread.Sleep(500);
    }
    finally
    {
        Console.WriteLine($"{nombre} terminó consulta.");
        _conexionBD.Release();
    }
}
```

🔍 Simula acceso controlado a la base de datos, útil para ambientes legacy.

✅ **¿Por qué `Semaphore`?**  
Clásico, interoperable, fácil de usar.

📊 **Comparación:**
- 🔁 `SemaphoreSlim`: mejor para apps modernas sin compatibilidad externa.
- 🔐 `lock`: no aplica aquí.

---

## 🧪 Ejemplo 9: Uso compartido de recursos físicos

```csharp
private static Semaphore _equipo = new(1, 1);

public static void UsarEquipo(string tecnico)
{
    _equipo.WaitOne();
    try
    {
        Console.WriteLine($"{tecnico} usando equipo compartido.");
        Thread.Sleep(400);
    }
    finally
    {
        Console.WriteLine($"{tecnico} dejó el equipo.");
        _equipo.Release();
    }
}
```

🔍 Acceso uno a uno a un recurso compartido como un scanner o lector.

✅ **¿Por qué `Semaphore`?**  
Fácil de implementar, seguro incluso para uso externo.

📊 **Comparación:**
- 🔁 `Mutex`: válido pero más costoso.
- 🔐 `lock`: más limitado.

---

## 🧪 Ejemplo 10: Coordinación de múltiples instancias del mismo proceso (multiplataforma y seguro)

```csharp
private static readonly Lazy<Semaphore> _semaforoGlobal = new(() =>
{
    if (OperatingSystem.IsWindows())
        return new Semaphore(2, 2, "Global\MiApp_Semaphore", out _);
    else
        return new Semaphore(2, 2); // En Linux/macOS no se permite nombrar el semáforo
});

public static void IniciarInstancia(string instancia)
{
    bool adquirido = false;

    try
    {
        adquirido = _semaforoGlobal.Value.WaitOne(1000);

        if (!adquirido)
        {
            Console.WriteLine($"{instancia} no pudo iniciar (límite alcanzado).");
            return;
        }

        Console.WriteLine($"{instancia} ejecutándose.");
        Thread.Sleep(800);
    }
    finally
    {
        if (adquirido)
        {
            _semaforoGlobal.Value.Release();
            Console.WriteLine($"{instancia} cerró sesión.");
        }
    }
}
```

🔍 Permite que solo 2 hilos o procesos ejecuten la instancia simultáneamente. Compatible con todos los sistemas operativos.

✅ **¿Por qué `Semaphore` con `Lazy`?**  
Evita errores de inicialización múltiple. `Lazy` asegura que el semáforo global se inicializa una sola vez de forma segura entre hilos.

📊 **Comparación:**
- 🔐 `lock`, `SemaphoreSlim`: no sirven entre procesos y no manejan múltiples instancias del sistema.
- 🧵 `Mutex`: posible, pero con mayor sobrecarga.
- 🌀 `Barrier`: no aplica. No es para limitar concurrencia, sino para sincronizar fases.

---