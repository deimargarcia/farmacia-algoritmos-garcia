n = 300.000 historias clínicas y q = 5.000 consultas.

i. Recorrer el listado por cada consulta: como máximo son n comparaciones por consulta. En total: 300.000 × 5.000 = 1.500.000.000 comparaciones.

ii. Ordenar una vez con un algoritmo O(n log n) y luego hacer búsqueda binaria: ordenar cuesta aproximadamente n log₂(n) ≈ 300.000 × 18,23 ≈ 5.476.000 operaciones. Las consultas cuestan aproximadamente q log₂(n) ≈ 5.000 × 18,23 ≈ 91.000 comparaciones. Total aproximado: 5.567.000 operaciones. La lista se ordena en el mismo espacio, sin duplicarla.

iii. Ordenar con un algoritmo cuadrático y luego buscar: ordenar cuesta aproximadamente n² = 300.000² = 90.000.000.000 operaciones. Las búsquedas binarias agregan unas 91.000 comparaciones. Total aproximado: 90.000.091.000 operaciones.

Elijo la opción 2. Aunque ordenar toma trabajo por la mañana, después cada consulta requiere muchas menos comparaciones. En P1.2, al duplicar la lista, la solución por pares tardó cerca de cuatro veces más; eso coincide con el crecimiento cuadrático. La cura, que ordena una copia, fue mucho más rápida, aunque en este caso no podemos duplicar el listado, así que usaría un algoritmo de ordenamiento que trabaje en el mismo espacio.

Cambiaría de opinión a favor de recorrer el listado si hubiera menos de unas 18 consultas: el ordenamiento cuesta cerca de 5.476.000 operaciones y cada recorrido completo cuesta hasta 300.000; 5.476.000 ÷ 300.000 ≈ 18,3. Con 19 consultas o más, ordenar y buscar binariamente requiere menos operaciones aproximadas.