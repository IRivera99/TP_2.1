//Creado por Ignacio Rivera
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int cantEspacios = 12;
            string opcion = "";
            List<Vehiculo> vehiculos;
            Estacionamiento estacionamientoFinito;
            Estacionamiento estacionamientoInfinito;
            Random randy = new Random();

            Console.WriteLine("Bienvenidos al estacionamiento cuántico!\n" +
                "Ingrese la cantidad de vehículos a generar (enter para aleatorio)");
            int.TryParse(Console.ReadLine(), out int cantVehiculos);
            InicializarEstacionamientos(cantVehiculos);

            while (!opcion.Equals("0"))
            {
                Console.WriteLine("\nSeleccione una opción\n" + 
                    "1- Listar todos los vehículos\n" + 
                    "2- Agregar un nuevo vehículo\n" + 
                    "3- Remover un vehículo en especial, dado su número de matrícula\n" +
                    "4- Remover un vehículo en especial, dado el dni de su dueño\n" +
                    "5- Remover una cantidad aleatoria de vehículos\n" +
                    "6- Optimizar el espacio\n" +
                    //"7- Debug\n" +
                    "Introduzca cualquier otra cosa para salir");
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        {
                            ListarVehiculos();
                            break;
                        }                        
                    case "2":
                        {
                            AgregarVehículo();
                            Console.WriteLine("\nVehículo agregado!");
                            break;
                        }                        
                    case "3":
                        {
                            Console.WriteLine("\nIngrese la matrícula del vehículo");
                            string matricula = Console.ReadLine().ToUpper();

                            if (EliminarVehiculo(matricula))
                                Console.WriteLine("Vehiculo eliminado con éxito!");
                            else
                                Console.WriteLine("Vehiculo no eliminado (puede que no exista)");

                            break;
                        }                        
                    case "4":
                        {
                            Console.WriteLine("\nIngrese DNI del dueño del vehículo");
                            string dni = Console.ReadLine();

                            if (EliminarVehiculo(dni))
                                Console.WriteLine("Vehiculo eliminado con éxito!");
                            else
                                Console.WriteLine("Vehiculo no eliminado (puede que no exista el dueño)");

                            break;
                        }                        
                    case "5":
                        {
                            Console.WriteLine("\nIngrese la cantidad de vehículos a eliminar (enter para aleatorio)");
                            int cantVehiculosEliminar;
                            int.TryParse(Console.ReadLine(), out cantVehiculosEliminar);
                            List<Vehiculo> vehiculosEliminados = EliminarVehiculos(cantVehiculosEliminar);
                            Console.WriteLine($"\nSe eliminaron {vehiculosEliminados.Count} autos:");
                            foreach (Vehiculo vehiculo in vehiculosEliminados)
                            {
                                Console.WriteLine($"Matricula: {vehiculo.Matricula}");
                            }
                            break;
                        }                        
                    case "6":
                        {
                            OptimizarEspacioEstacionamientos();
                            Console.WriteLine("\nEspacios optimizados!");
                            break;
                        }                        
                    /*case "7":
                        {
                            Debug();
                            break;
                        }*/
                    default:
                        {
                            Console.WriteLine("\nGracias por usar el estacionamiento cuántico, vuelva prontos!");
                            opcion = "0";
                            break;
                        }                        
                }
            }
            Console.ReadLine();

            void InicializarEstacionamientos(int cantidad)
            {
                estacionamientoFinito = new Estacionamiento(cantEspacios);
                estacionamientoInfinito = new Estacionamiento();
                vehiculos = new List<Vehiculo>();

                if (cantidad == 0)
                    cantidad = randy.Next(20, 60);

                for (int i = 0; i < cantidad; i++)
                {
                    vehiculos.Add(new Vehiculo());
                }

                foreach (Vehiculo vehiculo in vehiculos)
                {
                    EstacionarVehiculo(vehiculo, false);
                }
            }

            void ListarVehiculos()
            {
                Console.WriteLine("\nListado de vehículos:");
                foreach (Vehiculo vehiculo in vehiculos)
                {
                    Console.WriteLine($"Dueño: {vehiculo.Dueño.Nombre} DNI:{vehiculo.Dueño.Dni}\n" +
                        $"Modelo: {vehiculo.Modelo}\n" +
                        $"Matrícula: {vehiculo.Matricula}\n" +
                        $"Tamaño: {vehiculo.Tamaño} ({Math.Round(vehiculo.Largo, 2)} L x {Math.Round(vehiculo.Ancho, 2)} A)\n");
                }
                Console.WriteLine($"Cantidad de vehículos: {vehiculos.Count}");
            }

            void AgregarVehículo()
            {
                Vehiculo vehiculo = new Vehiculo();
                vehiculos.Add(vehiculo);
                EstacionarVehiculo(vehiculo, false);
            }

            bool EliminarVehiculo(string dato) 
            {
                bool eliminado = false;
                Vehiculo vehiculoEliminar = null;
                int.TryParse(dato, out int dni);

                foreach (Vehiculo vehiculo in vehiculos)
                {
                    if (vehiculo.Dueño.Dni == dni || vehiculo.Matricula.Equals(dato))
                    {
                        vehiculoEliminar = vehiculo;
                    }                        
                }

                if(vehiculoEliminar != null)
                {
                    vehiculos.Remove(vehiculoEliminar);
                    eliminado = true;
                }

                if (eliminado && !estacionamientoFinito.EliminarVehiculoEstacionado(vehiculoEliminar))
                    estacionamientoInfinito.EliminarVehiculoEstacionado(vehiculoEliminar);

                return eliminado;
            }            

            List<Vehiculo> EliminarVehiculos(int cantidad)
            {
                List<Vehiculo> vehiculosAEliminar = new List<Vehiculo>();
                List<Vehiculo> listaAuxiliar = new List<Vehiculo>();
                foreach (Vehiculo vehiculo in vehiculos)
                {
                    listaAuxiliar.Add(vehiculo);
                }

                if (cantidad > vehiculos.Count)
                {
                    cantidad = 0;
                    Console.WriteLine("\nExcediste el límite de vehículos, se tomará una cantidad aleatoria");
                }

                if (cantidad == 0)
                    cantidad = randy.Next(1, vehiculos.Count);                

                for (int i = 0; i < cantidad; i++)
                {
                    vehiculosAEliminar.Add(listaAuxiliar[randy.Next(0, listaAuxiliar.Count-1)]);
                    listaAuxiliar.Remove(vehiculosAEliminar[i]);
                }

                foreach (Vehiculo vehiculo in vehiculosAEliminar)
                {
                    EliminarVehiculo(vehiculo.Matricula);
                }

                return vehiculosAEliminar;
            }

            void OptimizarEspacioEstacionamientos()
            {
                foreach (Vehiculo vehiculo in vehiculos)
                {
                    if (!estacionamientoFinito.EliminarVehiculoEstacionado(vehiculo))
                        estacionamientoInfinito.EliminarVehiculoEstacionado(vehiculo);
                }
                foreach (Vehiculo vehiculo in vehiculos)
                {
                    EstacionarVehiculo(vehiculo, true);
                }
            }

            void EstacionarVehiculo(Vehiculo vehiculo, bool optimizado)
            {                
                if (!estacionamientoFinito.EstacionarVehiculo(vehiculo, optimizado))
                {
                    estacionamientoInfinito.EstacionarVehiculo(vehiculo, optimizado);
                }
            }

            /*void Debug()
            {
                Console.WriteLine("\nLugares\n" +
                    "Espacio finito");
                foreach (Espacio espacio in estacionamientoFinito.Espacios)
                {
                    Console.WriteLine($"Datos del espacio: {espacio.TipoDimension}, {espacio.TipoCliente}");
                    if (espacio.Ocupado)
                    {
                        //Console.WriteLine($"Ocupado por vehiculo {espacio.Vehiculo.Matricula} | DNI dueño: {espacio.Vehiculo.Dueño.Dni} | {espacio.Vehiculo.Tamaño}, {espacio.Vehiculo.Dueño.TipoCliente}");
                        Console.WriteLine($"Datos del vehiculo {espacio.Vehiculo.Tamaño}, {espacio.Vehiculo.Dueño.TipoCliente}");
                    }
                    else
                    {
                        Console.WriteLine("Desocupado");
                    }
                    Console.WriteLine("------------------------------------------------------------------------------");
                }

                Console.WriteLine("\nEspacio infinito");

                foreach (Espacio espacio in estacionamientoInfinito.Espacios)
                {
                    Console.WriteLine($"Datos del espacio: {espacio.TipoDimension}, {espacio.TipoCliente}");
                    if (espacio.Ocupado)
                    {
                        //Console.WriteLine($"Ocupado por vehiculo {espacio.Vehiculo.Matricula} | DNI dueño: {espacio.Vehiculo.Dueño.Dni} | {espacio.Vehiculo.Tamaño}, {espacio.Vehiculo.Dueño.TipoCliente}");
                        Console.WriteLine($"Datos del vehiculo {espacio.Vehiculo.Tamaño}, {espacio.Vehiculo.Dueño.TipoCliente}");
                    }
                    else
                    {
                        Console.WriteLine("Desocupado");
                    }
                    Console.WriteLine("------------------------------------------------------------------------------");
                }*/
        }
    }
}
