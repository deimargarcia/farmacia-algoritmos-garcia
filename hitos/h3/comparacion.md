H3: comparar fuerza bruta y programación dinámica

En la farmacia comparé dos formas de decidir qué productos reponer con un presupuesto limitado. Cada producto candidato tiene un costo y un puntaje de prioridad; se puede comprar un paquete de cada producto o dejarlo para después. El objetivo es elegir los paquetes que den el mayor puntaje sin pasar el presupuesto.

El programa genera datos de ejemplo reproducibles: los costos van de Bs 5 a Bs 30 y las prioridades de 20 a 120. El presupuesto es un tercio del costo total. Usé 3, 6, 12 y 24 productos. En cada tamaño, las dos soluciones recibieron los mismos datos. Antes de medir, el programa comprobó que ambas encontraran el mismo puntaje máximo y que sus paquetes elegidos respetaran el presupuesto.

Ejecuté en Release con: dotnet run -c Release --project hitos/h3/Comparacion.csproj

Calenté cada solución una vez. Para que los tiempos pequeños se midieran mejor, el programa ajustó las repeticiones de cada lote hasta acercarse a 100 ms; luego hizo tres lotes y calculó el tiempo promedio por ejecución.

| Productos | Presupuesto (Bs) | Puntaje máximo verificado | Dinámica: tres tiempos (ms) / promedio | Veces vs. fila anterior | Fuerza bruta: tres tiempos (ms) / promedio | Veces vs. fila anterior |
|---:|---:|---:|---|---:|---|---:|
| 3 | 13 | 90, coincide | 0,000581 / 0,000607 / 0,000551 · **0,000579** | — | 0,000214 / 0,000245 / 0,000215 · **0,000225** | — |
| 6 | 27 | 215, coincide | 0,001668 / 0,001385 / 0,001615 · **0,001556** | 2,69× | 0,001720 / 0,003062 / 0,002277 · **0,002353** | 10,46× |
| 12 | 66 | 533, coincide | 0,007528 / 0,006967 / 0,006891 · **0,007129** | 4,58× | 0,101303 / 0,103288 / 0,096369 · **0,100320** | 42,64× |
| 24 | 143 | 969, coincide | 0,026508 / 0,025955 / 0,022236 · **0,024900** | 3,49× | 328,629900 / 420,029800 / 387,548800 · **378,736167** | 3.775,28× |

Desde 6 productos, la dinámica creció entre 2,69× y 4,58× al duplicar el tamaño, cerca de O(n × B), ya que el presupuesto B también aumenta con n. La fuerza bruta creció 10,46×, 42,64× y 3.775,28× en las filas siguientes. En los tamaños pequeños se nota el ruido; por eso el programa mide lotes. Los puntajes coinciden en los cuatro tamaños y las dos selecciones respetan el presupuesto.
