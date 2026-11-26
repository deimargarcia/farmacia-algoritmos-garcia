H3: elegir qué productos reponer

Mi caso

En la farmacia puede haber varios productos con poco stock, pero el presupuesto de reposición no siempre alcanza para comprarlos todos. Para cada producto candidato considero un paquete de reposición, su costo en bolivianos y un puntaje de prioridad. El objetivo es elegir paquetes sin pasar el presupuesto y obtener el mayor puntaje. Cada paquete se elige una vez o se deja para otra compra.

Los puntajes son una forma sencilla de representar la urgencia de cada producto según su stock y movimiento. Para las mediciones usaré datos de ejemplo generados por el programa, no registros reales de una farmacia.

¿Voraz o programación dinámica?

Un voraz que elija primero el mayor puntaje por boliviano no siempre obtiene la mejor compra. Por ejemplo, con presupuesto de Bs 50 y estos paquetes:

| Paquete | Costo | Prioridad | Prioridad por boliviano |
|---|---:|---:|---:|
| A | Bs 10 | 60 | 6 |
| B | Bs 20 | 100 | 5 |
| C | Bs 30 | 120 | 4 |

El voraz elige A y B: suma 160 puntos y gasta Bs 30; ya no puede llevar C. La mejor combinación es B y C: suma 220 puntos y gasta los Bs 50. Este contraejemplo muestra que la elección local no garantiza el óptimo.

Sí aplica programación dinámica. Se repiten subproblemas al preguntar cuál es el mayor puntaje posible usando los primeros productos con cada presupuesto disponible. Para cada producto comparo dos opciones: dejarlo o incluirlo y completar con la mejor solución para el presupuesto restante. Así armo la solución completa a partir de resultados más pequeños.

La tabla tiene costo O(n × B) en tiempo y espacio, donde n es la cantidad de productos candidatos y B el presupuesto expresado en bolivianos enteros. La fuerza bruta revisa las combinaciones y cuesta O(n × 2ⁿ) en esta implementación.
