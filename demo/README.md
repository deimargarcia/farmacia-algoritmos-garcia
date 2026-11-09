# Demo en C# para órdenes de crecimiento y medición

Hice este programa pequeño para probar un proceso lineal y otro cuadrático, como vimos en clase. Los tiempos cambian según la computadora y la corrida.

Desde esta carpeta:

```powershell
dotnet run -c Release -- ordenes
dotnet run -c Release -- medicion
```

Con `medicion` se hace primero una corrida de calentamiento y luego se promedian cinco mediciones para cada tamaño: `n`, `4n` y `16n`. Se miden solo los ciclos del ejemplo.
