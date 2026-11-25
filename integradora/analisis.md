Deimar Omar Garcia Ventura

Variante A: furgoneta de reparto

 I1 — Analizar y elegir

a. Costo de la solución ingenua

PlanificadorIngenuo.Elegir recorre las 2ⁿ máscaras posibles. Para cada máscara revisa los n paquetes y suma sus pesos y ganancias. Por eso su tiempo es O(n × 2ⁿ). La memoria adicional es O(n), para guardar los códigos elegidos.

b. Recurrencia y teorema maestro

La versión recursiva considera dos opciones por paquete: llevarlo o dejarlo. Su recurrencia es T(n) = 2T(n−1) + 1. El teorema maestro de bolsillo no aplica porque exige subproblemas de tamaño n/b, con b mayor que 1; aquí el tamaño baja en uno, de n a n−1.

Al expandir la recurrencia, T(n) = 2ⁿT(0) + (2ⁿ − 1), así que esa versión recursiva cuesta O(2ⁿ). Este costo corresponde a la recurrencia indicada. El código ingenuo entregado, además, vuelve a recorrer los n paquetes para cada combinación; por eso su costo real es O(n × 2ⁿ).

c. Voraz y estrategia elegida

El voraz por ganancia por kilogramo no garantiza el mejor resultado. En el primer caso de prueba, la capacidad es 50 kg:

| Paquete | Peso | Ganancia | Ganancia por kg |
|---|---:|---:|---:|
| P1 | 10 kg | Bs 60 | 6 |
| P2 | 20 kg | Bs 100 | 5 |
| P3 | 30 kg | Bs 120 | 4 |

El voraz toma P1 y luego P2: gana Bs 160 y ocupa 30 kg; P3 ya no cabe. La mejor elección es P2 y P3: ocupa los 50 kg y gana Bs 220.

Elijo programación dinámica. Defino DP[i, c] como la máxima ganancia posible con los primeros i paquetes y capacidad c. Para cada paquete comparo dejarlo fuera con incluirlo y completar usando la mejor solución para la capacidad restante. Distintos caminos de la recursión pueden llegar al mismo par (i, c); la tabla guarda ese resultado y evita recalcularlo.

La recurrencia es DP[i, c] = DP[i−1, c] si el paquete i no cabe; si cabe, tomo el máximo entre DP[i−1, c] y Ganancia[i] + DP[i−1, c−Kg[i]]. El caso base es DP[0, c] = 0.

En el caso general, la tabla cuesta O(n(C + 1)) en tiempo y espacio, donde C es la capacidad en kg enteros; para C mayor que cero se suele escribir O(n × C). Reconstruir los códigos elegidos cuesta O(n), así que no cambia el costo total. Si el peso de todos los paquetes ya cabe en la furgoneta, el código tiene una salida directa de O(n): los elige todos sin construir la tabla.

d Consultar si un paquete va en el viaje

Después de elegir, guardo los códigos en un HashSet de cadenas. EstaEnElViaje usa Contains, que cuesta O(1) en promedio; revisar una lista con Contains cuesta O(n). La tabla hash es adecuada porque esta consulta se repetirá miles de veces. En el peor caso, muchas colisiones podrían llevar la búsqueda a O(n).
