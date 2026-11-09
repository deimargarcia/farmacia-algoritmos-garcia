# H1 — Búsqueda secuencial de un medicamento en inventario

## Proceso

Buscar un medicamento por su código recorriendo el inventario desde el primer registro hasta encontrarlo o llegar al final. Este análisis considera una búsqueda secuencial y no supone que el inventario esté ordenado.

## Quién sufre el problema

El auxiliar de farmacia y el farmacéutico, cuando necesitan localizar rápidamente un producto para atender una consulta o verificar existencias.

## Datos de muestra

Ejemplo didáctico de códigos (no corresponde a datos reales de una farmacia): `MED-001`, `MED-014`, `MED-027`, `MED-103`, `MED-205`. Buscar `MED-103` puede revisar cuatro registros; buscar un código inexistente revisa los cinco.

## Estimación

**O(n)** en el peor caso: la búsqueda puede revisar los n registros del inventario, por ejemplo, cuando el código está al final o no existe.
