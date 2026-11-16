using System.Diagnostics;
using System.Runtime.CompilerServices;

if (args.Length != 1 || args[0] is not ("c4" or "c5"))
{
    Console.WriteLine("Uso: dotnet run -c Release -- c4|c5");
    return;
}

if (args[0] == "c4")
    RunSearchComparison();
else
    RunSortTournament();

static void RunSearchComparison()
{
    int[] sizes = [1_000, 4_000, 16_000, 64_000];
    const int searchesPerBatch = 5_000;

    Console.WriteLine("C4 - Búsqueda lineal vs. binaria (Release)");
    Console.WriteLine("Calentamiento descartado; promedio de 3 corridas por medida.");
    Console.WriteLine($"Cada tiempo de búsqueda mide un lote de {searchesPerBatch:N0} consultas ausentes.");
    Console.WriteLine("n\tLineal ms\tBinaria ms\tCociente L/B\tOrdenar ms\tConsultas p/recuperar ordenamiento");

    foreach (int n in sizes)
    {
        int[] sorted = Enumerable.Range(0, n).Select(i => i * 2).ToArray();
        int[] queries = Enumerable.Range(0, searchesPerBatch).Select(i => 2 * (n + i) + 1).ToArray();
        int[] shuffled = Enumerable.Range(0, n).Select(i => i * 2).ToArray();
        Shuffle(shuffled, 31 + n);

        // Calentamiento: se ejecuta una vez y no entra en los promedios.
        _ = LinearSearchBatch(sorted, queries);
        _ = BinarySearchBatch(sorted, queries);
        SortCopy(shuffled);

        double linearMs = AverageMilliseconds(() => Measure(() => _ = LinearSearchBatch(sorted, queries)));
        double binaryMs = AverageMilliseconds(() => Measure(() => _ = BinarySearchBatch(sorted, queries)));
        double sortMs = AverageMilliseconds(() => SortCopy(shuffled));
        double ratio = binaryMs > 0 ? linearMs / binaryMs : double.PositiveInfinity;
        double breakEvenQueries = linearMs > binaryMs
            ? Math.Ceiling(sortMs / (linearMs - binaryMs) * searchesPerBatch)
            : double.PositiveInfinity;

        Console.WriteLine($"{n:N0}\t{linearMs:F4}\t\t{binaryMs:F4}\t\t{ratio:F2}\t\t{sortMs:F4}\t\t{breakEvenQueries:N0}");
    }

    Console.WriteLine("Nota: ambas búsquedas usan el mismo arreglo ya ordenado; ordenar se mide aparte.");
}

static void RunSortTournament()
{
    int[] sizes = [1_000, 4_000, 16_000];
    const int arraysPerRun = 10;
    Action<int[]>[] algorithms = [InsertionSort, MergeSort, QuickSort];
    string[] names = ["Inserción ms", "MergeSort ms", "QuickSort ms"];

    Console.WriteLine("C5 - Torneo de ordenamientos (Release)");
    Console.WriteLine("Arreglos aleatorios con semilla fija; clonar datos fuera del cronómetro.");
    Console.WriteLine("Calentamiento descartado; promedio de 3 corridas por algoritmo y tamaño.");
    Console.WriteLine($"n\t{names[0]}\t{names[1]}\t{names[2]}");

    foreach (int n in sizes)
    {
        int[] input = Enumerable.Range(0, n).Select(i => i).ToArray();
        Shuffle(input, 2026 + n);
        double[] times = new double[algorithms.Length];

        for (int algorithmIndex = 0; algorithmIndex < algorithms.Length; algorithmIndex++)
        {
            Action<int[]> sort = algorithms[algorithmIndex];
            int[] warmup = (int[])input.Clone();
            sort(warmup);
            GC.KeepAlive(warmup);

            times[algorithmIndex] = AverageMilliseconds(() =>
            {
                int[][] copies = Enumerable.Range(0, arraysPerRun)
                    .Select(_ => (int[])input.Clone()).ToArray();
                var timer = Stopwatch.StartNew();
                foreach (int[] copy in copies)
                    sort(copy);
                timer.Stop();
                if (copies.Any(copy => !IsSorted(copy)))
                    throw new InvalidOperationException("El algoritmo no dejó el arreglo ordenado.");
                GC.KeepAlive(copies);
                return timer.Elapsed.TotalMilliseconds / arraysPerRun;
            });
        }

        Console.WriteLine($"{n:N0}\t{times[0]:F4}\t\t{times[1]:F4}\t\t{times[2]:F4}");
    }
}

static double AverageMilliseconds(Func<double> measure)
{
    const int repetitions = 3;
    double total = 0;
    for (int run = 0; run < repetitions; run++)
        total += measure();
    return total / repetitions;
}

static double Measure(Action action)
{
    var timer = Stopwatch.StartNew();
    action();
    timer.Stop();
    return timer.Elapsed.TotalMilliseconds;
}

[MethodImpl(MethodImplOptions.NoInlining)]
static int LinearSearchBatch(int[] values, int[] queries)
{
    int found = 0;
    foreach (int query in queries)
        for (int i = 0; i < values.Length; i++)
            if (values[i] == query)
            {
                found++;
                break;
            }
    return found;
}

[MethodImpl(MethodImplOptions.NoInlining)]
static int BinarySearchBatch(int[] values, int[] queries)
{
    int found = 0;
    foreach (int query in queries)
    {
        int low = 0;
        int high = values.Length - 1;
        while (low <= high)
        {
            int middle = low + (high - low) / 2;
            if (values[middle] == query)
            {
                found++;
                break;
            }
            if (values[middle] < query)
                low = middle + 1;
            else
                high = middle - 1;
        }
    }
    return found;
}

static double SortCopy(int[] source)
{
    int[] copy = (int[])source.Clone();
    var timer = Stopwatch.StartNew();
    Array.Sort(copy);
    timer.Stop();
    GC.KeepAlive(copy);
    return timer.Elapsed.TotalMilliseconds;
}

static void Shuffle(int[] values, int seed)
{
    var random = new Random(seed);
    for (int i = values.Length - 1; i > 0; i--)
    {
        int j = random.Next(i + 1);
        (values[i], values[j]) = (values[j], values[i]);
    }
}

[MethodImpl(MethodImplOptions.NoInlining)]
static void InsertionSort(int[] values)
{
    for (int i = 1; i < values.Length; i++)
    {
        int current = values[i];
        int j = i - 1;
        while (j >= 0 && values[j] > current)
        {
            values[j + 1] = values[j];
            j--;
        }
        values[j + 1] = current;
    }
}

[MethodImpl(MethodImplOptions.NoInlining)]
static void MergeSort(int[] values)
{
    int[] buffer = new int[values.Length];
    MergeSortRange(values, buffer, 0, values.Length - 1);
}

static void MergeSortRange(int[] values, int[] buffer, int left, int right)
{
    if (left >= right) return;
    int middle = left + (right - left) / 2;
    MergeSortRange(values, buffer, left, middle);
    MergeSortRange(values, buffer, middle + 1, right);
    int i = left, j = middle + 1, k = left;
    while (i <= middle && j <= right)
        buffer[k++] = values[i] <= values[j] ? values[i++] : values[j++];
    while (i <= middle) buffer[k++] = values[i++];
    while (j <= right) buffer[k++] = values[j++];
    Array.Copy(buffer, left, values, left, right - left + 1);
}

[MethodImpl(MethodImplOptions.NoInlining)]
static void QuickSort(int[] values) => QuickSortRange(values, 0, values.Length - 1);

static void QuickSortRange(int[] values, int left, int right)
{
    while (left < right)
    {
        int pivotIndex = Partition(values, left, right);
        // Recurse into the smaller side first to limit call-stack depth.
        if (pivotIndex - left < right - pivotIndex)
        {
            QuickSortRange(values, left, pivotIndex - 1);
            left = pivotIndex + 1;
        }
        else
        {
            QuickSortRange(values, pivotIndex + 1, right);
            right = pivotIndex - 1;
        }
    }
}

static int Partition(int[] values, int left, int right)
{
    int pivot = values[right];
    int boundary = left;
    for (int i = left; i < right; i++)
        if (values[i] <= pivot)
        {
            (values[boundary], values[i]) = (values[i], values[boundary]);
            boundary++;
        }
    (values[boundary], values[right]) = (values[right], values[boundary]);
    return boundary;
}

static bool IsSorted(int[] values)
{
    for (int i = 1; i < values.Length; i++)
        if (values[i - 1] > values[i])
            return false;
    return true;
}
