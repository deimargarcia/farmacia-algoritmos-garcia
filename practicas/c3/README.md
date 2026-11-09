# C3 — Medición empírica de crecimiento

En el taller comparé dos procesos: uno con un ciclo y otro con dos ciclos anidados. Los ejecuté con .NET en modo Release. Primero hice una corrida de calentamiento, que no conté; después repetí cada medición cinco veces y saqué el promedio. Usé tamaños n, 4n y 16n.

Los tamaños son ejemplos para la prueba. Los tiempos son de mi corrida y pueden cambiar un poco si la vuelvo a ejecutar o si uso otra computadora.

## Tabla de mediciones

| n | Proceso lineal (ms, promedio de 5) | Proceso cuadrático (ms, promedio de 5) |
|---:|---:|---:|
| 1 000 | 0,0126 | 2,0869 |
| 4 000 | 0,1283 | 18,6497 |
| 16 000 | 0,0388 | 264,3824 |

En esta corrida el proceso de dos ciclos tardó más al aumentar el tamaño. Los tiempos del proceso lineal son muy pequeños y salieron irregulares, así que la tabla muestra lo que pasó en esta prueba; no significa que siempre dará exactamente esos tiempos.

Para repetir la medición desde `demo/`:

```powershell
dotnet run -c Release -- medicion
```

## Cómo se relaciona con el código

- `LinearProcess` tiene un ciclo que se repite n veces, por eso es O(n).
- `QuadraticProcess` tiene dos ciclos, uno dentro del otro, por eso es O(n²).
- O grande describe el peor caso. Las constantes se descartan porque no cambian la forma de crecimiento.
