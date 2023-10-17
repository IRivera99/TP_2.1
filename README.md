Los Laboratorios Apertura, una importante empresa de
investigación científica mixta, nos ha pedido un programa para
su nuevo sistema de estacionamiento cuántico.
El estacionamiento está dividido en 2 partes, una tiene un
espacio finito de exactamente 12 posiciones, donde las
posiciones 3, 7 y 12 están reservadas para VIP (personas de alta importancia) y solo
pueden ocuparse por vehículos cuyos dueños sean VIP.
El otro, nos han comentado, tiene posiciones infinitas y puede guardar N vehiculos, sin
embargo, las posiciones de este estacionamiento infinito no son simétricas y pueden tener
diferentes tamaños. Esos tamaños pueden ser mini, standard o max, donde un vehículo
más pequeño puede entrar en un espacio más grande pero no es ideal.
Se nos pide que intentemos primero llenar el estacionamiento regular antes de ocupar el estacionamiento cuántico, pero no hace falta mover los vehículos una vez estacionados.

De los vehículos sabemos sus atributos básicos, como su modelo, su dueño, su matrícula, y sus dimensiones guardadas por ancho y largo.

Si un vehículo tiene menos de 4 metros de largo y 1.5 de ancho, se considera mini. Si tiene entre 4 y 5 metros de largo y 1.5 a 2 de ancho, se considera standard, y si excede cualquiera de las dimensiones se considera max.

Nos piden que el sistema pueda generar automáticamente una gran cantidad de vehículos y posiciones del estacionamiento con atributos aleatorios por motivos de testing y simulación.

Luego, nos piden las siguientes funcionalidades:
1) Listar todos los vehículos
2) Agregar un nuevo vehículo - sus atributos pueden ser aleatorios.
3) Remover un vehículo en especial, dado su número de matrícula
4) Remover un vehículo en especial, dado el dni de su dueño
5) Remover una cantidad aleatoria de vehículos.
6) Optimizar el espacio, moviendo todos los vehículos que no estén ocupando una casilla correspondiente de su espacio actual a uno nuevo. Los laboratorios Apertura nos permiten hacer uso de su otro estacionamiento infinito (un espacio temporal) donde guardar los vehículos mientras se realiza está operación.