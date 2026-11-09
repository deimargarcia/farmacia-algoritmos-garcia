using System.Diagnostics;
using System.Runtime.CompilerServices;

const int warmupN = 1000;
int[] sizes = [1000, 4000, 16000];
const int repetitions = 5;

if (args.Length != 1 || args[0] is not ("ordenes" or "medicion"))
{
    Console.WriteLine("Uso: dotnet run -c Release -- ordenes|medicion");
    return;
}

if (args[0] == "ordenes")
{
    Console.WriteLine("Un ciclo que recorre n elementos: O(n).");
    Console.WriteLine("Dos ciclos anidados que recorren n elementos: O(n²).");
    Console.WriteLine("Las constantes se descartan porque no cambian la forma de crecimiento.");
    Console.WriteLine("O grande describe el peor caso.");
    return;
}

// Calentamiento descartado: se ejecutan ambos procesos antes de tomar tiempos.
_ = LinearProcess(warmupN);
_ = QuadraticProcess(warmupN);

Console.WriteLine("Medición empírica en esta máquina (.NET Release)");
Console.WriteLine("Se descarta el calentamiento; cada dato es el promedio de 5 corridas.");
Console.WriteLine("n\tLineal (ms)\tCuadrático (ms)");

foreach (int n in sizes)
{
    double linearMs = AverageMilliseconds(LinearProcess, n);
    double quadraticMs = AverageMilliseconds(QuadraticProcess, n);
    Console.WriteLine($"{n}\t{linearMs:F4}\t\t{quadraticMs:F4}");
}

static double AverageMilliseconds(Func<int, long> process, int n)
{
    double total = 0;
    for (int run = 0; run < repetitions; run++)
    {
        var timer = Stopwatch.StartNew();
        _ = process(n);
        timer.Stop();
        total += timer.Elapsed.TotalMilliseconds;
    }
    return total / repetitions;
}

[MethodImpl(MethodImplOptions.NoInlining)]
static long LinearProcess(int n)
{
    long operations = 0;
    for (int i = 0; i < n; i++)
        operations++;
    return operations;
}

[MethodImpl(MethodImplOptions.NoInlining)]
static long QuadraticProcess(int n)
{
    long operations = 0;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            operations++;
    return operations;
}
