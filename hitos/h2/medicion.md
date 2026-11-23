# H2: medición de búsqueda en la farmacia

Comparé buscar un producto por código en una lista simple y en una tabla hash. Es una de las operaciones que más se repite en la farmacia. Cada corrida hizo 100.000 consultas del mismo producto, que estaba al final de la lista para medir el caso más lento de esa búsqueda. Usé 150, 300, 600 y 1.200 productos.

Trabajé en Release: calenté una vez cada solución y descarté esos tiempos; después hice tres corridas por tamaño y calculé el promedio. Para repetir la medición ejecuté este comando desde la carpeta del repositorio:

dotnet run -c Release --project hitos/h2/medicion.csproj

| Productos | Lista: tres tiempos (ms) / promedio | Veces vs. fila anterior | Tabla hash: tres tiempos (ms) / promedio | Veces vs. fila anterior |
|---:|---|---:|---|---:|
| 150 | 146,6629 / 161,8584 / 184,2673 · **164,2629** | — | 1,6856 / 1,9381 / 2,8063 · **2,1433** | — |
| 300 | 331,6549 / 370,9471 / 341,3262 · **347,9761** | 2,12× | 2,1229 / 3,5957 / 2,4051 · **2,7079** | 1,26× |
| 600 | 843,5849 / 884,1028 / 767,4869 · **831,7249** | 2,39× | 2,0866 / 2,1010 / 1,9223 · **2,0366** | 0,75× |
| 1.200 | 1.119,5944 / 1.219,3704 / 1.306,4429 · **1.215,1359** | 1,46× | 1,0797 / 1,7774 / 6,5185 · **3,1252** | 1,53× |

La lista tardó más al crecer los datos, aunque sus tiempos no se duplicaron de forma pareja; influyeron la caché y el ruido de la computadora. La tabla hash se mantuvo en pocos milisegundos, con una corrida más ruidosa en el último tamaño. Los resultados siguen la tendencia esperada: O(n) para la lista y O(1) promedio para la tabla hash.
