# C3 — Medición empírica de crecimiento

La demo compara un proceso lineal (un ciclo) y uno cuadrático (dos ciclos anidados). Se ejecutó con .NET en configuración Release. Se descartó una corrida de calentamiento, se promediaron cinco corridas por tamaño y se escalaron los datos como n, 4n y 16n.

Los datos de entrada son tamaños sintéticos; los tiempos son de esta máquina y pueden variar entre corridas.

## Tabla de mediciones

| n | Proceso lineal (ms, promedio de 5) | Proceso cuadrático (ms, promedio de 5) |
|---:|---:|---:|
| 1 000 | 0,0126 | 2,0869 |
| 4 000 | 0,1283 | 18,6497 |
| 16 000 | 0,0388 | 264,3824 |

La tendencia del proceso cuadrático se hace visible al crecer n; los tiempos lineales de esta corrida son demasiado pequeños y variables para mostrar una progresión limpia. Por eso esta tabla se interpreta como evidencia de una corrida concreta, no como una garantía ni como sustituto del análisis teórico.

Para repetir la medición desde `demo/`:

```powershell
dotnet run -c Release -- medicion
```

## Relación entre código y orden

- `LinearProcess`: un ciclo que visita n iteraciones, O(n).
- `QuadraticProcess`: dos ciclos anidados de n iteraciones, O(n²).
- O grande describe el peor caso; se descartan constantes porque no cambian la forma de crecimiento.
