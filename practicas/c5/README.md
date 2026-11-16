# C5: torneo de ordenamientos

Comparé inserción, que tiene dos ciclos y es cuadrático en el peor caso, con MergeSort y QuickSort. Usé arreglos con los mismos valores en orden aleatorio y repetí el experimento con cantidades crecientes.

## Protocolo

- Programa en C# ejecutado con `dotnet run -c Release -- c5` desde `demo/`.
- Cantidades: 1.000, 4.000 y 16.000 números.
- Los datos se mezclan con una semilla fija para poder repetir la corrida.
- Descarté el calentamiento y promedié 3 corridas por algoritmo y tamaño.
- Cada corrida ordena 10 copias nuevas; el tiempo de la tabla es el promedio por arreglo. La copia de los datos se prepara antes de iniciar el cronómetro.

## Tabla de mi corrida

| Elementos | Inserción (ms) | MergeSort (ms) | QuickSort (ms) |
|---:|---:|---:|---:|
| 1.000 | 0,3986 | 0,4765 | 0,3016 |
| 4.000 | 4,3852 | 1,3220 | 1,5796 |
| 16.000 | 77,3797 | 1,9097 | 1,3712 |

## Qué observo

Con 1.000 elementos los tres tardaron parecido. En 16.000 elementos, inserción tardó unas 41 veces lo que MergeSort y 56 veces lo que QuickSort en esta corrida. MergeSort y QuickSort fueron bastante más rápidos que inserción para estos arreglos.

Esto no significa que QuickSort siempre tenga ese tiempo: en el peor caso puede ser O(n²), aunque su rendimiento promedio suele ser O(n log n). MergeSort es O(n log n) también en el peor caso, pero necesita memoria adicional. Inserción tiene peor caso O(n²) y usa poca memoria; puede servir para listas pequeñas o casi ordenadas.

Los milisegundos cambian según la computadora y la corrida. Para generar otra tabla: `dotnet run -c Release -- c5`.
