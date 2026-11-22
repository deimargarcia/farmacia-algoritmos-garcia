Deimar Omar Garcia Ventura

P2.1: elección de estructuras

En la pastelería, los pedidos se preparan según su plazo: primero sale el más urgente. Si dos tienen el mismo plazo, preparo primero el que llegó antes. También necesito saber si un pedido todavía sigue pendiente.

| Operación | Veces al día | Qué elegí | Costo | Con una lista simple |
|---|---:|---|---|---|
| Registrar un pedido | 120.000 | Cola de prioridad por plazo y llegada, más un HashSet con los nombres | O(log n) en la cola y O(1) promedio en el conjunto | Agregar al final cuesta O(1) amortizado |
| Preparar el siguiente | 120.000 | PriorityQueue y HashSet | O(log n) en la cola y O(1) promedio para quitar el nombre | Buscar el más urgente y quitarlo cuesta O(n) |
| Consultar si sigue pendiente | 40.000 | HashSet<string> | O(1) promedio | Hay que recorrer la lista; cuesta O(n) |
| Consultar cuántos quedan | 240.000, como supuesto: una consulta después de cada registro y preparación | PriorityQueue.Count | O(1) | List.Count también cuesta O(1) |

Elegí la cola de prioridad porque me permite preparar primero el pedido más urgente. Uso el número de llegada como segundo criterio para respetar el desempate. El HashSet guarda los nombres que aún no se prepararon y me permite consultarlos rápido.

 ii. Comparación de costos para la jornada

Con 120.000 pedidos, la lista tiene que volver a buscar el más urgente cada vez que preparo uno. Esas búsquedas suman aproximadamente n(n−1)/2, o **7.199.940.000 comparaciones**. Además, al quitar un pedido puede tener que desplazar otros.

Las 40.000 consultas de pendientes podrían revisar hasta 120.000 nombres cada una: una cota de **4.800.000.000 comprobaciones** si se hacen con la lista llena. En conjunto, buscar los pedidos urgentes y consultar nombres podría superar los **12.000 millones de comprobaciones**, sin contar los desplazamientos.

Con mi solución, registrar y preparar cuestan O(log n) cada uno. Como log₂(120.000) es aproximadamente 17, las 120.000 inserciones y preparaciones representan hasta unos **4,1 millones de niveles de heap**. Las altas, bajas y consultas del HashSet son 280.000 operaciones de O(1) promedio. Consultar cuántos quedan cuesta O(1) tanto en la cola como en la lista.

La lista no es mala para agregar pedidos al final ni para consultar cuántos hay. Se vuelve lenta en las tareas que más importan aquí: encontrar el pedido más urgente y comprobar si un nombre sigue pendiente.

iii.Cuándo elegiría otra estructura

Si todos los pedidos se prepararan en el orden en que llegaron y el plazo no importara, usaría una cola FIFO. Registrar y preparar costarían O(1). Mantendría el HashSet si todavía necesitara consultar si un pedido sigue pendiente.
