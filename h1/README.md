# H1: buscar un medicamento en el inventario

## Proceso

El proceso que elegí es buscar un medicamento por su código. Se revisa el inventario desde el primer registro hasta encontrarlo. Para este ejemplo supongo que la lista no está ordenada.

## Quién sufre el problema

Este problema le puede pasar al auxiliar de farmacia o al farmacéutico cuando necesitan encontrar un producto para atender a un cliente o revisar si hay stock.

## Datos de muestra

Para mostrarlo, uso estos códigos de ejemplo: `MED-001`, `MED-014`, `MED-027`, `MED-103` y `MED-205`. No son datos reales. Si busco `MED-103`, reviso cuatro registros. Si el código no está en la lista, reviso los cinco.

## Estimación

**O(n)** en el peor caso, porque podría tener que revisar todos los productos, por ejemplo si el que busco está al final o no aparece en el inventario.
