# H2: medición de búsqueda en la farmacia

Comparé buscar un producto por código en una lista simple y en una tabla hash. Es una de las operaciones que más se repite en la farmacia. Cada corrida hizo 100.000 consultas del mismo producto, que estaba al final de la lista para medir el caso más lento de esa búsqueda. Usé 150, 300, 600 y 1.200 productos.

Trabajé en Release: calenté una vez cada solución y descarté esos tiempos; después hice tres corridas por tamaño y calculé el promedio. Para repetir la medición ejecuté este comando desde la carpeta del repositorio:

dotnet run -c Release --project hitos/h2/medicion.csproj

| Productos | Lista: tres tiempos (ms) / promedio | Veces vs. fila anterior | Tabla hash: tres tiempos (ms) / promedio | Veces vs. fila anterior |
|---:|---|---:|---|---:|
| 150 | 191,4156 / 178,3855 / 199,7215 · **189,8409** | — | 2,7915 / 2,4720 / 2,8673 · **2,7103** | — |
| 300 | 337,8599 / 380,6388 / 497,6551 · **405,3846** | 2,14× | 1,8405 / 2,9173 / 2,0867 · **2,2815** | 0,84× |
| 600 | 861,9374 / 753,4113 / 844,5930 · **819,9806** | 2,02× | 1,6637 / 2,4032 / 1,9976 · **2,0215** | 0,89× |
| 1.200 | 1.526,5681 / 1.567,5185 / 1.414,2306 · **1.502,7724** | 1,83× | 1,7263 / 2,1018 / 6,4594 · **3,4292** | 1,70× |

La lista creció cerca de dos veces al duplicar los datos, como se espera de O(n), aunque el último factor fue menor. La tabla hash se mantuvo alrededor de 2 a 3 ms; el último tamaño tuvo una medición ruidosa. En esta corrida, la tendencia coincide con O(1) promedio para la tabla hash, pero los milisegundos variaron entre repeticiones.
