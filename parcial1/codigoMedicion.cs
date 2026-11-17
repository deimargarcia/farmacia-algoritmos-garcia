using System.Diagnostics;

namespace Parcial1.VarianteA;

public static class Registro
{
    // Solución ACTUAL (lenta): comparar cada carnet con todos los que le siguen.
    public static bool HayRepetidosPorPares(int[] carnets)
    {
        for (int i = 0; i < carnets.Length; i++)
            for (int j = i + 1; j < carnets.Length; j++)
                if (carnets[i] == carnets[j]) return true;
        return false;
    }

    // TU CURA: escribe aquí una versión correcta que cueste MENOS que O(n²).
    // No modifiques la lista que recibes (trabaja sobre una copia si la necesitas ordenada).
    public static bool HayRepetidosCurado(int[] carnets)
    {
        int[] copia = (int[])carnets.Clone();
        Array.Sort(copia);

        for (int i = 1; i < copia.Length; i++)
        {
            if (copia[i] == copia[i - 1])
                return true;
        }

        return false;
    }

    // El PEOR CASO para las dos: una lista SIN repetidos (hay que mirarlo todo).
    public static int[] GenerarSinRepetidos(int n, int semilla = 42)
    {
        var datos = new int[n];
        for (int i = 0; i < n; i++) datos[i] = i;
        var azar = new Random(semilla);
        for (int i = n - 1; i > 0; i--)
        {
            int k = azar.Next(i + 1);
            (datos[i], datos[k]) = (datos[k], datos[i]);
        }
        return datos;
    }
}

public static class Programa
{
    // Los cinco casos que tu cura debe pasar (la esperada va a la derecha).
    private static readonly (int[] datos, bool esperado, string que)[] Casos =
    {
        (new[] { 7, 3, 9, 3, 5 },   true,  "repetido en el medio"),
        (new[] { 7, 3, 9, 4, 5 },   false, "sin repetidos"),
        (new int[] { },             false, "lista vacía"),
        (new[] { 8 },               false, "un solo elemento"),
        (new[] { 4, 1, 2, 6, 4 },   true,  "repetido en los extremos"),
    };

    public static void Main()
    {
        bool casosCorrectos = true;
        foreach (var (datos, esperado, que) in Casos)
        {
            bool lenta = Registro.HayRepetidosPorPares(datos);
            bool curada = Registro.HayRepetidosCurado(datos);
            bool correcto = lenta == esperado && curada == esperado;
            casosCorrectos &= correcto;
            Console.WriteLine($"{que,-26} esperado {esperado,-5} · por pares {lenta,-5} · tu cura {curada,-5} · {(correcto ? "OK" : "ERROR")}");
        }

        Console.WriteLine($"Casos: {(casosCorrectos ? "todos correctos" : "hay errores")}");

        // Cuatro tamaños duplicados; datos y arreglos se crean fuera del tiempo medido.
        const int n = 5_000;
        int[] tamanos = { n, 2 * n, 4 * n, 8 * n };
        int[][] entradas = tamanos.Select(t => Registro.GenerarSinRepetidos(t)).ToArray();

        // Una corrida de calentamiento por solución y tamaño, descartada.
        for (int i = 0; i < tamanos.Length; i++)
        {
            _ = Registro.HayRepetidosPorPares(entradas[i]);
            _ = Registro.HayRepetidosCurado(entradas[i]);
        }

        Console.WriteLine("\nMediciones en segundos (3 corridas; promedio)");
        Console.WriteLine("n\tPares\t× anterior\tCurado\t× anterior");
        double promedioParesAnterior = 0;
        double promedioCuradoAnterior = 0;

        for (int i = 0; i < tamanos.Length; i++)
        {
            double[] pares = new double[3];
            double[] curado = new double[3];
            for (int corrida = 0; corrida < 3; corrida++)
            {
                var reloj = Stopwatch.StartNew();
                _ = Registro.HayRepetidosPorPares(entradas[i]);
                reloj.Stop();
                pares[corrida] = reloj.Elapsed.TotalSeconds;

                reloj.Restart();
                _ = Registro.HayRepetidosCurado(entradas[i]);
                reloj.Stop();
                curado[corrida] = reloj.Elapsed.TotalSeconds;
            }

            double promedioPares = pares.Average();
            double promedioCurado = curado.Average();
            string factorPares = i == 0 ? "—" : (promedioPares / promedioParesAnterior).ToString("F2");
            string factorCurado = i == 0 ? "—" : (promedioCurado / promedioCuradoAnterior).ToString("F2");
            Console.WriteLine($"{tamanos[i]}\t{string.Join(" / ", pares.Select(x => x.ToString("F4")))} (avg {promedioPares:F4})\t{factorPares}\t{string.Join(" / ", curado.Select(x => x.ToString("F4")))} (avg {promedioCurado:F4})\t{factorCurado}");
            promedioParesAnterior = promedioPares;
            promedioCuradoAnterior = promedioCurado;
        }
    }
}
