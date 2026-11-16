# C6: elegir un ordenamiento para mi caso

## Mi proceso y mis datos

Mi caso es la gestión de productos de una farmacia. Para completar esta parte con mis datos reales:

- Productos registrados hoy: **[falta indicar cantidad]**.
- Mayor cantidad de productos que manejo en un mes: **[falta indicar cantidad]**.
- Operación que más pesa en el proceso: **[buscar / ordenar / comparar; falta confirmar]**.

## Algoritmos que considero

| Candidato | Peor caso | Comentario |
|---|---|---|
| Inserción | O(n²) | Usa poca memoria y puede convenir con pocos datos o una lista casi ordenada. |
| MergeSort | O(n log n) | Mantiene ese orden de crecimiento en el peor caso, a cambio de usar memoria auxiliar. |

QuickSort también se midió en [C5](../c5/README.md): normalmente es rápido, pero su peor caso es O(n²). La tabla completa y el protocolo están en [C5](../c5/README.md); la comparación de búsquedas está en [C4](../c4/README.md).

## Mi elección y evidencia

**Borrador pendiente de los datos del caso.** En la corrida de C5, MergeSort tardó 1,9097 ms con 16.000 elementos y tuvo O(n log n) incluso en el peor caso. Inserción tardó 77,3797 ms con el mismo tamaño y su peor caso es O(n²). QuickSort fue más rápido en esa corrida, pero su peor caso es O(n²). Si el inventario llega a miles de productos y ordenar es una operación importante, MergeSort parece una opción razonable si la memoria auxiliar no es un problema. Antes de dejar esta elección como conclusión final, necesito confirmar el tamaño real del inventario y qué operación ocurre más.

## Cómo voy a medirlo

Voy a usar como tamaños el inventario actual y el máximo del mes más cargado; agregaré tamaños intermedios para ver la tendencia. En cada tamaño prepararé los mismos datos para ambos candidatos, haré un calentamiento descartado, mediré en Release y promediaré 3 corridas, como en C5. Registraré los milisegundos en una tabla y compararé también el peor caso teórico.
