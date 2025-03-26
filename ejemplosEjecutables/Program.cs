
using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WriteLine("🧪 Ejecutando ejemplos de Semaphore...");

        Console.WriteLine("----------Ejemplo 1----------");
        for (int i = 1; i <= 4; i++)
            new Thread(() => SemaphoreExamples.AccederRecurso($"Hilo{i}")).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 2----------");
        for (int i = 1; i <= 2; i++)
            new Thread(() => SemaphoreExamples.EscribirArchivo($"Hilo{i}")).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 3----------");
        for (int i = 1; i <= 3; i++)
            new Thread(() => SemaphoreExamples.IngresarZona($"Usuario{i}")).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 4----------");
        for (int i = 1; i <= 2; i++)
            new Thread(() => SemaphoreExamples.Imprimir($"Doc{i}")).Start();
        Thread.Sleep(1200);

        Console.WriteLine("----------Ejemplo 5----------");
        for (int i = 1; i <= 4; i++)
            new Thread(() => SemaphoreExamples.ManejarConexion($"Cliente{i}")).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 6----------");
        for (int i = 1; i <= 2; i++)
            new Thread(() => SemaphoreExamples.UsarEstacion($"Operario{i}")).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 7----------");
        for (int i = 1; i <= 5; i++)
            new Thread(() => SemaphoreExamples.ProcesarLote($"Tarea{i}")).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 8----------");
        for (int i = 1; i <= 3; i++)
            new Thread(() => SemaphoreExamples.ConsultarBD($"Hilo{i}")).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 9----------");
        for (int i = 1; i <= 2; i++)
            new Thread(() => SemaphoreExamples.UsarEquipo($"Técnico{i}")).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 10----------");
        for (int i = 1; i <= 3; i++)
            new Thread(() => SemaphoreExamples.IniciarInstancia($"Instancia{i}")).Start();
        Thread.Sleep(1500);

        Console.WriteLine("✅ Fin de los ejemplos.");
    }
}
