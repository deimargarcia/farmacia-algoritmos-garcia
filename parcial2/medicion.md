Deimar Omar Garcia Ventura


P2.3: medición

Para comparar las dos soluciones usé EjecutarCarga: primero registra n pedidos y después prepara esos mismos n. Lo ejecuté en Release con 8.000, 16.000 y 32.000 pedidos. En cada tamaño calenté una vez cada solución y descarté ese tiempo; luego hice tres corridas y saqué el promedio. Los factores muestran cuánto creció cada promedio respecto al tamaño anterior.

```powershell
dotnet run -c Release --project medicion.csproj

| n | Ingenua: tres tiempos (s), promedio | Crecimiento | Mejorada: tres tiempos (s), promedio | Crecimiento |
|---:|---|---:|---|---:|
| 8.000 | 0,1680 / 0,1593 / 0,1653; **0,1642** | — | 0,0051 / 0,0062 / 0,0039; **0,0051** | — |
| 16.000 | 0,7556 / 0,7563 / 0,7360; **0,7493** | 4,56× | 0,0074 / 0,0195 / 0,0088; **0,0119** | 2,34× |
| 32.000 | 3,3339 / 4,3277 / 2,7152; **3,4589** | 4,62× | 0,0230 / 0,0391 / 0,0310; **0,0310** | 2,61× |

Al duplicar los pedidos, la ingenua tardó unas 4,6 veces más, parecido al crecimiento esperado de O(n²). La mejorada creció entre 2,3 y 2,6 veces, una tendencia compatible con O(n log n). Hubo algo de variación entre corridas, sobre todo en la mejorada; el tiempo más alto fue 4,3277 s para la ingenua con 32.000 pedidos, dentro del intervalo solicitado.
