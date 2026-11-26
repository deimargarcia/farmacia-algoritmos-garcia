# C6: elegir un ordenamiento para mi caso

## Mi proceso y mis datos

En el inventario de la farmacia se busca y actualiza el stock de unos 1.000 productos; en el mes más cargado calculo que podrían ser 1.200. Tomo como base las estimaciones de H2: con al menos 50 ventas al día y dos productos por venta, habría unas 100 búsquedas y actualizaciones diarias. Una vez al día también puedo ordenar los productos por stock para preparar la lista de reposición.

## Candidatos

| Algoritmo | Costo en el peor caso | Qué significa para mi caso |
|---|---|---|
| Inserción | O(n²) | Usa poca memoria y puede ser buena con pocos datos o si ya están casi ordenados. |
| MergeSort | O(n log n) | Mantiene ese costo aunque los datos lleguen desordenados; necesita memoria auxiliar. |

## Mi elección y la evidencia

Elijo **MergeSort** para ordenar el inventario por stock. En C5, con 1.000 elementos, inserción tardó 0,3986 ms y MergeSort 0,4765 ms: la diferencia fue menor a una décima de milisegundo. Con 4.000, MergeSort tardó 1,3220 ms frente a 4,3852 ms de inserción; con 16.000, 1,9097 ms frente a 77,3797 ms.

Hoy tengo alrededor de 1.000 productos y ordenar es una tarea diaria, así que la pequeña diferencia medida en 1.000 elementos no pesa mucho. Prefiero el costo O(n log n) garantizado de MergeSort si el inventario crece o los productos llegan en un orden desfavorable. La memoria auxiliar me parece aceptable para este tamaño.

## Cómo lo mediría con los datos de la farmacia

Compararía inserción y MergeSort con listas de 1.000, 1.100 y 1.200 productos, en el mismo orden inicial y con los mismos datos. Haría una corrida de calentamiento descartada y luego tres corridas medidas en Release; reportaría el promedio. Prepararía las copias antes de iniciar el cronómetro y verificaría que ambos algoritmos dejen la lista ordenada.
