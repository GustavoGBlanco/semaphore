
using System;
using System.Threading;

public static class SemaphoreExamples
{
    private static Semaphore _semaforo = new(2, 2);
    private static Semaphore _archivo = new(1, 1);
    private static Semaphore _zonaCritica = new(2, 2);
    private static Semaphore _impresora = new(1, 1);
    private static Semaphore _conexiones = new(3, 3);
    private static Semaphore _estacion = new(1, 1);
    private static Semaphore _lote = new(3, 3);
    private static Semaphore _conexionBD = new(2, 2);
    private static Semaphore _equipo = new(1, 1);

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
    
    private static readonly Lazy<Semaphore> _semaforoGlobal = new(() => {
        if (OperatingSystem.IsWindows())
            return new Semaphore(2, 2, "Global\\MiApp_Semaphore", out _);
        else
            return new Semaphore(2, 2);
    });

    public static void IniciarInstancia(string instancia) {
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
}
