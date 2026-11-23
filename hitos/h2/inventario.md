# H2: inventario de una farmacia

## Mi caso y mis datos

Voy a trabajar con el inventario de una farmacia. El auxiliar busca productos para atender las ventas y actualiza el stock. Si no encuentra rápido un código, la atención se demora; si no descuenta lo vendido, el inventario deja de ser confiable.

Para este ejercicio uso un escenario estimado: **50 ventas al día como mínimo**, con unos **2 productos por venta**. Eso da alrededor de **100 consultas y actualizaciones de stock al día**. Supongo **1.000 productos hoy** y hasta **1.200 en el mes más cargado**. Son cifras inventadas para el caso, no datos medidos de una farmacia concreta.

Cada producto tiene código, nombre y cantidad disponible.

## Operaciones del inventario

| Operación | Frecuencia estimada | Para qué la hago |
|---|---:|---|
| Buscar por código | Unas 100 veces al día | Encontrar el producto de una venta |
| Actualizar stock | Unas 100 veces al día, más reposiciones | Descontar lo vendido o sumar lo recibido |
| Insertar un producto | Unas 2 veces por semana | Registrar un producto nuevo |
| Borrar un producto | Alrededor de 1 vez por semana | Retirar un producto que ya no se vende |
| Encontrar el menor stock o el siguiente | Una vez al día | Revisar qué producto necesita reposición primero |
| Buscar un rango de stock | Una vez al día | Ver los productos entre dos cantidades de stock |
| Recorrer en orden de stock | Una vez al día | Revisar el inventario desde el menor stock al mayor |

Las frecuencias son estimaciones para el ejercicio. Las búsquedas por código y las actualizaciones son las operaciones que más se repiten; las altas, bajas y revisiones ordenadas son menos frecuentes.
