Deimar Omar Garcia Ventura

i3 — Medición

Medí EjecutarCarga en Release con 18, 20, 22 y 24 paquetes. En cada tamaño hice una corrida de calentamiento y la descarté; luego medí tres corridas, una llamada por corrida, y saqué el promedio. Comparé la ganancia de las dos soluciones en cada tamaño; coincidió en las cuatro mediciones.

| Paquetes | Ganancia (ambas) | Ingenua: 3 tiempos (ms) / promedio | Veces vs. fila anterior | Mejorada: 3 tiempos (ms) / promedio | Veces vs. fila anterior |
|---:|---:|---|---:|---|---:|
| 18 | 511 | 20,4891 / 27,2061 / 27,4650 · **25,0534** | — | 0,3639 / 0,2631 / 13,4992 · **4,7087** | — |
| 20 | 520 | 103,9339 / 102,3614 / 87,2730 · **97,8561** | 3,91× | 0,3297 / 0,2937 / 0,1519 · **0,2584** | 0,05× |
| 22 | 743 | 424,8948 / 376,3466 / 461,6875 · **420,9763** | 4,30× | 0,2849 / 0,1622 / 0,2098 · **0,2190** | 0,85× |
| 24 | 1.040 | 1.539,2274 / 1.447,8922 / 1.446,0061 · **1.477,7086** | 3,51× | 0,1451 / 0,1535 / 0,1438 · **0,1475** | 0,67× |

Al sumar dos paquetes, la ingenua tardó entre 3,51 y 4,30 veces más, cerca de lo esperado al cuadruplicarse las combinaciones. La mejorada fue mucho más rápida, aunque sus tiempos son tan cortos que el ruido se nota: por ejemplo, una corrida de 18 paquetes salió bastante más alta que las otras. La mayor medida de la ingenua fue 1.539,2274 ms con 24 paquetes, dentro del intervalo pedido; las ganancias coinciden en todos los tamaños.

ii. Solución mejorada con 1.000 paquetes y capacidad de 10.000 kg

Usé 1.000 paquetes generados con semilla fija. Descarté una corrida de calentamiento y medí tres veces solo "Elegir" (sin contar la generación de los paquetes): 80,0413 ms, 87,8155 ms y 77,8308 ms. El promedio fue **81,8959 ms** y la ganancia obtenida, **32.754 Bs**.

iii. Si la capacidad fuera 1.000.000.000 kg

Con los 1.000 paquetes de la prueba anterior, el peso total es menor de 59.000 kg. Si la capacidad subiera a 1.000.000.000 kg, todos cabrían y la implementación los devolvería en O(n), sin crear la tabla dinámica.

Si los paquetes no cupieran todos, la tabla dependería de la cantidad de paquetes y de la capacidad: con 1.000 paquetes y mil millones de kg serían aproximadamente 1.001.000.001.001 celdas int, cerca de 4 TB de memoria. No sería viable; habría que usar otra estrategia o limitar la capacidad que se procesa.
