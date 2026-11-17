Deimar Omar Garcia Ventura

Programa Complementario 2026

F1
total=0
for i=1 to n;
    total = total + dato[i]

R: O(n) el ciclo pasa una vez por cada dato, asi que el trabajo aumenta en proporcion al tamaño de la lista

F2
cuenta = 0
for i = 1 to n;
    for j = i + 1 to n:
        if dato[i] == dato[j]: cuenta = cuenta +1

R: O(n2): se compara cada dato con los que estan despues. Cuando la lista crece, aumenta mucho la cantidad de pares que hay que revisar

F3
k = n 
while k > 1:
    k = k / 2 
    paso = paso + 1

R: O(log n): En cada vuelta, k se divide entre dos. Por eso el ciclo necesita muchas menos vueltas que la cantidad de datos.

F4
for i = 1 to n:
    k = 1
    while k < n:
         k = k * 2
R: O(n log n): el ciclo de afuera se repite n veces. En cada repeticion, el de adentro duplica k hasta llegar a n.

b.Dos recurrencias (2 puntos, 1 c/u). Resuelve con el teorema maestro de bolsillo (T(1) = 1): escribe a, b, d, la comparación de a con b^d y la forma.

(i)   T(n) = 3 T(n/2) + n

Recurrencia = 3 T(n/2)+n
a=3 b=2 d=1
b^d=2
Forma=n²

(ii)    T(n) = T(n/2) + n²

Recurrencia = T(n/2) + n²
a=1 b=2 d=2
b^d=4
Forma=n²


