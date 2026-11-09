# Demo en C# para órdenes de crecimiento y medición

Este proyecto de consola ilustra un proceso lineal y otro cuadrático, siguiendo el protocolo compartido en clase. Las mediciones dependen de la máquina y de la corrida; no son valores esperados universales.

Desde esta carpeta:

```powershell
dotnet run -c Release -- ordenes
dotnet run -c Release -- medicion
```

El modo `medicion` descarta un calentamiento, mide tamaños `n`, `4n` y `16n` y promedia cinco corridas por tamaño. Los tiempos reportados solo corresponden a los bucles medidos en este demo.
