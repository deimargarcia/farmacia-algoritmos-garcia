# C4: búsqueda lineal y búsqueda binaria

Quise comparar cuánto tardan las dos búsquedas en encontrar un dato que no está en la lista. Al no encontrarlo, la búsqueda lineal recorre todos los elementos; la binaria descarta la mitad en cada paso. Las dos trabajan sobre el mismo arreglo ordenado. También medí aparte cuánto tarda en ordenarse una copia, para estimar cuándo se recupera ese costo.

## Cómo hice la medición

- Programa en C# ejecutado con `dotnet run -c Release -- c4` desde `demo/`.
- Probé arreglos de 1.000, 4.000, 16.000 y 64.000 elementos.
- En cada tamaño medí un lote de 5.000 búsquedas de valores ausentes.
- Hice un calentamiento que descarté y promedié 3 corridas para cada tiempo.
- El cociente es tiempo lineal dividido entre tiempo binario. “Consultas para recuperar el ordenamiento” estima cuántas búsquedas hacen falta para compensar el tiempo de ordenar una vez.

## Resultados de mi corrida

| Elementos | Lineal: 5.000 búsquedas (ms) | Binaria: 5.000 búsquedas (ms) | Cociente lineal/binaria | Ordenar una vez (ms) | Consultas aprox. para recuperar el ordenamiento |
|---:|---:|---:|---:|---:|---:|
| 1.000 | 6,3319 | 0,2132 | 29,70 | 0,0869 | 72 |
| 4.000 | 20,0709 | 0,1718 | 116,80 | 0,2633 | 67 |
| 16.000 | 86,2879 | 0,2312 | 373,27 | 1,3989 | 82 |
| 64.000 | 318,1075 | 0,1498 | 2.123,08 | 3,7746 | 60 |

## ¿Conviene ordenar primero?

En esta prueba sí conviene si voy a buscar muchas veces: el costo de ordenar se recupera después de unas 60 a 82 consultas, según el tamaño. Si fuera una sola búsqueda o el arreglo cambiara todo el tiempo, haría búsqueda lineal para no pagar el ordenamiento cada vez.

Los tiempos son de esta computadora y pueden variar. Para repetir la tabla, ejecuto el comando indicado arriba.
