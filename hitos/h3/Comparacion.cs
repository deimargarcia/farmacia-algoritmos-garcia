using System.Diagnostics;
using System.Globalization;

public readonly record struct PaqueteReposicion(int Costo, int Prioridad);
public readonly record struct ResultadoReposicion(int Prioridad, int[] IndicesElegidos);

public static class Programa
{
    private static readonly int[] Tamanos = { 3, 6, 12, 24 };
    private const double DuracionLoteMs = 100;

    public static void Main()
    {
        Console.WriteLine("Reposición de farmacia: máximo puntaje sin pasar el presupuesto.");
        Console.WriteLine("Tiempos por ejecución en ms; un calentamiento y tres lotes medidos.");
        Console.WriteLine("n\tBs\tóptimo verificado\tlote D/B\tdinámica: 3 tiempos / promedio / x anterior\tfuerza bruta: 3 tiempos / promedio / x anterior");

        double anteriorDinamica = 0;
        double anteriorBruta = 0;

        foreach (int n in Tamanos)
        {
            PaqueteReposicion[] paquetes = CrearPaquetes(n);
            int presupuesto = paquetes.Sum(p => p.Costo) / 3;
            ResultadoReposicion resultadoDinamica = ResolverDinamica(paquetes, presupuesto);
            ResultadoReposicion resultadoBruta = ResolverFuerzaBruta(paquetes, presupuesto);

            if (resultadoDinamica.Prioridad != resultadoBruta.Prioridad ||
                !EsValida(resultadoDinamica, paquetes, presupuesto) ||
                !EsValida(resultadoBruta, paquetes, presupuesto))
                throw new InvalidOperationException($"Los resultados no coinciden para n={n}.");

            Func<ResultadoReposicion> ejecutarDinamica = () => ResolverDinamica(paquetes, presupuesto);
            Func<ResultadoReposicion> ejecutarBruta = () => ResolverFuerzaBruta(paquetes, presupuesto);
            _ = ejecutarDinamica();
            _ = ejecutarBruta();
            int loteDinamica = Calibrar(ejecutarDinamica);
            int loteBruta = Calibrar(ejecutarBruta);

            double[] tiemposDinamica = Enumerable.Range(0, 3)
                .Select(_ => MedirLote(ejecutarDinamica, loteDinamica)).ToArray();
            double[] tiemposBruta = Enumerable.Range(0, 3)
                .Select(_ => MedirLote(ejecutarBruta, loteBruta)).ToArray();

            double promedioDinamica = tiemposDinamica.Average();
            double promedioBruta = tiemposBruta.Average();
            string factorDinamica = n == Tamanos[0]
                ? "—"
                : (promedioDinamica / anteriorDinamica).ToString("F2", CultureInfo.InvariantCulture) + "x";
            string factorBruta = n == Tamanos[0]
                ? "—"
                : (promedioBruta / anteriorBruta).ToString("F2", CultureInfo.InvariantCulture) + "x";

            Console.WriteLine(
                $"{n}\t{presupuesto}\t{resultadoDinamica.Prioridad} (coincide)\t{loteDinamica}/{loteBruta}\t" +
                $"{Formato(tiemposDinamica)} / {Formato(promedioDinamica)} / {factorDinamica}\t" +
                $"{Formato(tiemposBruta)} / {Formato(promedioBruta)} / {factorBruta}");

            anteriorDinamica = promedioDinamica;
            anteriorBruta = promedioBruta;
        }
    }

    private static PaqueteReposicion[] CrearPaquetes(int n)
    {
        var azar = new Random(2026 + n);
        return Enumerable.Range(0, n)
            .Select(_ => new PaqueteReposicion(azar.Next(5, 31), azar.Next(20, 121)))
            .ToArray();
    }

    private static ResultadoReposicion ResolverDinamica(PaqueteReposicion[] paquetes, int presupuesto)
    {
        var tabla = new int[paquetes.Length + 1, presupuesto + 1];

        for (int i = 1; i <= paquetes.Length; i++)
        {
            PaqueteReposicion paquete = paquetes[i - 1];
            for (int disponible = 0; disponible <= presupuesto; disponible++)
            {
                tabla[i, disponible] = tabla[i - 1, disponible];
                if (paquete.Costo <= disponible)
                {
                    int conPaquete = tabla[i - 1, disponible - paquete.Costo] + paquete.Prioridad;
                    tabla[i, disponible] = Math.Max(tabla[i, disponible], conPaquete);
                }
            }
        }

        var elegidos = new List<int>();
        int capacidadRestante = presupuesto;
        for (int i = paquetes.Length; i > 0; i--)
        {
            if (tabla[i, capacidadRestante] == tabla[i - 1, capacidadRestante])
                continue;

            elegidos.Add(i - 1);
            capacidadRestante -= paquetes[i - 1].Costo;
        }

        elegidos.Reverse();
        return new ResultadoReposicion(tabla[paquetes.Length, presupuesto], elegidos.ToArray());
    }

    private static ResultadoReposicion ResolverFuerzaBruta(PaqueteReposicion[] paquetes, int presupuesto)
    {
        int mejor = 0;
        int[] mejoresIndices = Array.Empty<int>();
        var elegidos = new List<int>();

        void Explorar(int i, int costo, int prioridad)
        {
            if (i == paquetes.Length)
            {
                if (costo <= presupuesto && prioridad > mejor)
                {
                    mejor = prioridad;
                    mejoresIndices = elegidos.ToArray();
                }
                return;
            }

            Explorar(i + 1, costo, prioridad);
            elegidos.Add(i);
            Explorar(i + 1, costo + paquetes[i].Costo, prioridad + paquetes[i].Prioridad);
            elegidos.RemoveAt(elegidos.Count - 1);
        }

        Explorar(0, 0, 0);
        return new ResultadoReposicion(mejor, mejoresIndices);
    }

    private static bool EsValida(ResultadoReposicion resultado, PaqueteReposicion[] paquetes, int presupuesto)
    {
        int costo = resultado.IndicesElegidos.Sum(i => paquetes[i].Costo);
        int prioridad = resultado.IndicesElegidos.Sum(i => paquetes[i].Prioridad);
        return costo <= presupuesto && prioridad == resultado.Prioridad;
    }

    private static int Calibrar(Func<ResultadoReposicion> accion)
    {
        int repeticiones = 1;
        while (true)
        {
            var reloj = Stopwatch.StartNew();
            long control = 0;
            for (int i = 0; i < repeticiones; i++) control += accion().Prioridad;
            reloj.Stop();
            GC.KeepAlive(control);
            if (reloj.Elapsed.TotalMilliseconds >= DuracionLoteMs || repeticiones >= 1_048_576)
                return repeticiones;
            repeticiones *= 2;
        }
    }

    private static double MedirLote(Func<ResultadoReposicion> accion, int repeticiones)
    {
        var reloj = Stopwatch.StartNew();
        long control = 0;
        for (int i = 0; i < repeticiones; i++) control += accion().Prioridad;
        reloj.Stop();
        GC.KeepAlive(control);
        return reloj.Elapsed.TotalMilliseconds / repeticiones;
    }

    private static string Formato(IEnumerable<double> tiempos) =>
        string.Join(" / ", tiempos.Select(Formato));

    private static string Formato(double tiempo) =>
        tiempo.ToString("F6", CultureInfo.InvariantCulture);
}
