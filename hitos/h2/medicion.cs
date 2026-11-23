using System.Diagnostics;
using System.Globalization;

public sealed record Producto(string Codigo, string Nombre, int Stock);

public sealed class InventarioIngenuo
{
    private readonly List<Producto> _productos;

    public InventarioIngenuo(IEnumerable<Producto> productos) => _productos = productos.ToList();

    public Producto? Buscar(string codigo)
    {
        foreach (Producto producto in _productos)
            if (producto.Codigo == codigo)
                return producto;

        return null;
    }
}

public sealed class InventarioAdecuado
{
    private readonly Dictionary<string, Producto> _porCodigo;

    public InventarioAdecuado(IEnumerable<Producto> productos) =>
        _porCodigo = productos.ToDictionary(p => p.Codigo, StringComparer.Ordinal);

    public Producto? Buscar(string codigo) =>
        _porCodigo.TryGetValue(codigo, out Producto? producto) ? producto : null;
}

public static class Programa
{
    private const int ConsultasPorCorrida = 100_000;
    private const int Repeticiones = 3;

    public static void Main()
    {
        int[] tamanos = { 150, 300, 600, 1_200 };
        double anteriorIngenuo = 0;
        double anteriorAdecuado = 0;

        Console.WriteLine("Búsqueda del mismo código, ubicado al final de la lista.");
        Console.WriteLine($"Cada corrida hace {ConsultasPorCorrida:N0} consultas.");
        Console.WriteLine("n\tLista: 3 tiempos (ms), promedio\t× anterior\tTabla hash: 3 tiempos (ms), promedio\t× anterior");

        foreach (int n in tamanos)
        {
            List<Producto> datos = CrearProductos(n);
            var ingenuo = new InventarioIngenuo(datos);
            var adecuado = new InventarioAdecuado(datos);
            string codigoBuscado = datos[^1].Codigo;

            if (ingenuo.Buscar(codigoBuscado) is null || adecuado.Buscar(codigoBuscado) is null ||
                ingenuo.Buscar("NO-EXISTE") is not null || adecuado.Buscar("NO-EXISTE") is not null)
                throw new InvalidOperationException("Las dos búsquedas no dieron el mismo resultado.");

            _ = EjecutarLote(ingenuo.Buscar, codigoBuscado);
            _ = EjecutarLote(adecuado.Buscar, codigoBuscado);

            double[] tiemposIngenuo = new double[Repeticiones];
            double[] tiemposAdecuado = new double[Repeticiones];
            for (int i = 0; i < Repeticiones; i++)
            {
                tiemposIngenuo[i] = Medir(ingenuo.Buscar, codigoBuscado);
                tiemposAdecuado[i] = Medir(adecuado.Buscar, codigoBuscado);
            }

            double promedioIngenuo = tiemposIngenuo.Average();
            double promedioAdecuado = tiemposAdecuado.Average();
            string factorIngenuo = n == tamanos[0] ? "—" : (promedioIngenuo / anteriorIngenuo).ToString("F2", CultureInfo.InvariantCulture);
            string factorAdecuado = n == tamanos[0] ? "—" : (promedioAdecuado / anteriorAdecuado).ToString("F2", CultureInfo.InvariantCulture);

            Console.WriteLine($"{n}\t{Formato(tiemposIngenuo)}; prom. {promedioIngenuo.ToString("F4", CultureInfo.InvariantCulture)}\t{factorIngenuo}\t{Formato(tiemposAdecuado)}; prom. {promedioAdecuado.ToString("F4", CultureInfo.InvariantCulture)}\t{factorAdecuado}");
            anteriorIngenuo = promedioIngenuo;
            anteriorAdecuado = promedioAdecuado;
        }
    }

    private static List<Producto> CrearProductos(int n)
    {
        var productos = Enumerable.Range(0, n)
            .Select(i => new Producto($"MED-{i:D5}", $"Producto {i}", i % 30))
            .ToArray();
        var azar = new Random(42 + n);

        for (int i = productos.Length - 1; i > 0; i--)
        {
            int j = azar.Next(i + 1);
            (productos[i], productos[j]) = (productos[j], productos[i]);
        }

        return productos.ToList();
    }

    private static long EjecutarLote(Func<string, Producto?> buscar, string codigo)
    {
        long encontrados = 0;
        for (int i = 0; i < ConsultasPorCorrida; i++)
            if (buscar(codigo) is not null)
                encontrados++;
        return encontrados;
    }

    private static double Medir(Func<string, Producto?> buscar, string codigo)
    {
        var reloj = Stopwatch.StartNew();
        long encontrados = EjecutarLote(buscar, codigo);
        reloj.Stop();
        GC.KeepAlive(encontrados);
        return reloj.Elapsed.TotalMilliseconds;
    }

    private static string Formato(double[] tiempos) =>
        string.Join(" / ", tiempos.Select(t => t.ToString("F4", CultureInfo.InvariantCulture)));
}
