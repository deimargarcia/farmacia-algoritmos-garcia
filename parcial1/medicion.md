

La cura realiza una copia del arreglo original, ordena los carnets mediante Array.Sort y compara elementos consecutivos para detectar duplicados, sin modificar la lista recibida. Su costo temporal es O(n log n).

Hice las mediciones en **Release**, con listas sin repetidos. Para cada tamaño ejecuté una corrida de calentamiento que no conté y después hice tres corridas medidas; la tabla muestra esos tres tiempos en segundos y su promedio. Los factores comparan el promedio de esa fila con el de la fila anterior.

| n | Por pares (3 tiempos; promedio) | Veces más que la fila anterior | Cura (3 tiempos; promedio) | Veces más que la fila anterior |
|---:|---:|---:|---:|---:|
| 5.000 | 0,0323 / 0,0294 / 0,0379 (**0,0332**) | — | 0,0012 / 0,0019 / 0,0041 (**0,0024**) | — |
| 10.000 | 0,0860 / 0,0808 / 0,0827 (**0,0831**) | 2,50× | 0,0021 / 0,0020 / 0,0011 (**0,0018**) | 0,74× |
| 20.000 | 0,3469 / 0,3535 / 0,3003 (**0,3336**) | 4,01× | 0,0035 / 0,0022 / 0,0024 (**0,0027**) | 1,53× |
| 40.000 | 1,5354 / 1,4540 / 1,1987 (**1,3960**) | 4,18× | 0,0050 / 0,0042 / 0,0045 (**0,0046**) | 1,69× |


Al duplicar la lista, por pares se acerca a multiplicar su tiempo por cuatro, como se espera de O(n²). La cura crece mucho menos; sus tiempos pequeños tienen algo de ruido y no siguen una proporción exacta, aunque aumentan gradualmente, como cabe esperar de O(n log n). La corrida más lenta fue por pares, con **1,5354 s para n = 40.000**; su promedio para ese tamaño fue 1,3960 s.
