# H2: estructuras para el inventario

Para la farmacia elijo una tabla hash para buscar por código y un árbol balanceado para las consultas de stock. Son las estructuras de los temas de parcial2 que encajan con estas operaciones; no necesito usar pila, cola o heap solo por incluirlos.

| Operación | Frecuencia estimada | Estructura | Costo | Motivo |
|---|---:|---|---|---|
| Buscar por código | Unas 100 veces al día | Tabla hash (Dictionary de C#) | O(1) promedio | Es una búsqueda frecuente y devuelve el producto directamente. |
| Insertar producto | Unas 2 veces por semana | Tabla hash y árbol por stock | O(log n) en total, en promedio | Se guarda por código y también en el índice ordenado. |
| Borrar producto | Alrededor de 1 vez por semana | Tabla hash y árbol por stock | O(log n) en total, en promedio | Se elimina de las dos estructuras. |
| Actualizar stock | Unas 100 veces al día | Tabla hash y árbol por stock | O(log n) en total, en promedio | Se actualiza el producto y su lugar en el árbol. |
| Encontrar el menor o el siguiente stock | Una vez al día | Árbol balanceado | O(log n) | El árbol mantiene los productos ordenados por stock. |
| Buscar un rango de stock | Una vez al día | Árbol balanceado | O(log n + k) | Se llega al inicio del rango y se recorren sus k productos. |
| Recorrer por stock | Una vez al día | Árbol balanceado | O(n) | Se visitan los productos desde el menor stock al mayor. |

La tabla hash es necesaria porque relaciona el código con los datos del producto; un conjunto solo indicaría si el código existe. Su costo O(1) es promedio: si hay muchas colisiones, una búsqueda puede llegar a O(n). El árbol balanceado conserva el orden por stock y usa el código para desempatar. Una lista simple tendría que revisar productos para buscar por código y ordenar o recorrer todo para consultar rangos. Un heap serviría para sacar el menor, pero no para listar rangos ni recorrer el inventario ordenado.

La medición compara la búsqueda por código de una lista simple con la de la tabla hash, que es la operación más repetida. El árbol queda elegido para las consultas ordenadas del inventario.
