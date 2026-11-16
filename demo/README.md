# Demo en C# para órdenes de crecimiento y medición

Hice este programa pequeño para correr las mediciones de C4 y C5 en C#. Los tiempos cambian según la computadora y la corrida.

Desde esta carpeta:

```powershell
dotnet run -c Release -- c4
dotnet run -c Release -- c5
```

`c4` compara búsqueda lineal y binaria, y calcula por separado el costo de ordenar. `c5` compara inserción, MergeSort y QuickSort. En ambos casos se descarta el calentamiento y se promedian tres corridas.
