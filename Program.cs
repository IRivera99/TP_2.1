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
                            Vehiculo vehiculo = new Vehiculo(false)
                            {
                                Matricula = Console.ReadLine().ToUpper()
                            };

                            if (EliminarVehiculo(vehiculo))
                                Console.WriteLine("Vehiculo eliminado con éxito!");
                            else
                                Console.WriteLine("Vehiculo no eliminado (puede que no exista)");

                            break;
                        }                        
                    case "4":
                        {
                            Console.WriteLine("\nIngrese DNI del dueño del vehículo");
                            int.TryParse(Console.ReadLine(), out int dni);

                            Vehiculo vehiculo = new Vehiculo(false)
                            {
                                Dueño = new Persona(false)
                                {
                                    Dni = dni
                                }
                            };

                            if (EliminarVehiculo(vehiculo))
                                Console.WriteLine("Vehiculo eliminado con éxito!");
                            else
                                Console.WriteLine("Vehiculo no eliminado (puede que no exista el dueño)");

                            break;
                        }                        
                    case "5":
                        {
                            Console.WriteLine("\nIngrese la cantidad de vehículos a eliminar (enter para aleatorio)");
                            int.TryParse(Console.ReadLine(), out int cantVehiculosEliminar);
                            List<Vehiculo> vehiculosEliminados = EliminarVehiculos(cantVehiculosEliminar);
                            Console.WriteLine($"\nSe eliminaron {vehiculosEliminados.Count} vehiculos:");
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
                List<Vehiculo> vehiculos = new List<Vehiculo>();

                if (cantidad == 0)
                    cantidad = randy.Next(20, 60);

                for (int i = 0; i < cantidad; i++)
                {
                    vehiculos.Add(new Vehiculo(true));
                }

                foreach (Vehiculo vehiculo in vehiculos)
                {
                    EstacionarVehiculo(vehiculo, false);
                }
            }

            List<Vehiculo> ObtenerListaVehiculos(bool listaParaOptimizar)
            {
                List<Vehiculo> vehiculos = new List<Vehiculo>();

                foreach (Espacio espacio in estacionamientoFinito.Espacios)
                {
                    if (espacio.Ocupado && (!listaParaOptimizar || 
                        (listaParaOptimizar && (int)espacio.TipoDimension != (int)espacio.Vehiculo.Tamaño)))
                        vehiculos.Add(espacio.Vehiculo);                    
                }

                foreach (Espacio espacio in estacionamientoInfinito.Espacios)
                {
                    if (espacio.Ocupado && (!listaParaOptimizar ||
                        (listaParaOptimizar && (int)espacio.TipoDimension != (int)espacio.Vehiculo.Tamaño)))
                        vehiculos.Add(espacio.Vehiculo);
                }

                return vehiculos;
            }

            void ListarVehiculos()
            {
                List<Vehiculo> vehiculos = ObtenerListaVehiculos(false);

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
                Vehiculo vehiculo = new Vehiculo(true);
                EstacionarVehiculo(vehiculo, false);
            }

            bool EliminarVehiculo(Vehiculo vehiculoAEliminar) 
            {
                bool eliminado = false;
                List<Vehiculo> vehiculos = ObtenerListaVehiculos(false);

                if (!vehiculoAEliminar.Matricula.Equals(string.Empty) && vehiculoAEliminar != null)
                    vehiculoAEliminar = vehiculos.Find(v => v.Matricula == vehiculoAEliminar.Matricula);

                if (vehiculoAEliminar.Dueño.Dni != 0 && vehiculoAEliminar != null)
                    vehiculoAEliminar = vehiculos.Find(v => v.Dueño.Dni == vehiculoAEliminar.Dueño.Dni);

                eliminado = EliminarVehiculoEstacionado(vehiculoAEliminar);                   

                return eliminado;
            }            

            List<Vehiculo> EliminarVehiculos(int cantidad)
            {
                List<Vehiculo> vehiculosAEliminar = new List<Vehiculo>();
                List<Vehiculo> vehiculos = ObtenerListaVehiculos(false);

                if (cantidad > vehiculos.Count)
                {
                    cantidad = 0;
                    Console.WriteLine("\nExcediste el límite de vehículos, se tomará una cantidad aleatoria");
                }

                if (cantidad == 0)
                    cantidad = randy.Next(1, vehiculos.Count);                

                for (int i = 0; i < cantidad; i++)
                {
                    vehiculosAEliminar.Add(vehiculos[randy.Next(0, vehiculos.Count-1)]);
                    vehiculos.Remove(vehiculosAEliminar[i]);
                }

                foreach (Vehiculo vehiculo in vehiculosAEliminar)
                {
                    EliminarVehiculo(vehiculo);
                }

                return vehiculosAEliminar;
            }

            void OptimizarEspacioEstacionamientos()
            {
                List<Vehiculo> vehiculosAOptimizar = ObtenerListaVehiculos(true);

                foreach (Vehiculo vehiculo in vehiculosAOptimizar)
                {
                    EliminarVehiculoEstacionado(vehiculo);
                }

                foreach (Vehiculo vehiculo in vehiculosAOptimizar)
                {               
                    EstacionarVehiculo(vehiculo, true);
                }
            }

            bool EstacionarVehiculo(Vehiculo vehiculo, bool optimizado)
            {
                bool estacionado = false;

                if (estacionamientoFinito.EstacionarVehiculo(vehiculo, optimizado) || estacionamientoInfinito.EstacionarVehiculo(vehiculo, optimizado))
                    estacionado = true;

                return estacionado;
            }

            bool EliminarVehiculoEstacionado(Vehiculo vehiculo)
            {
                bool eliminado = false;

                if (estacionamientoFinito.EliminarVehiculoEstacionado(vehiculo) || estacionamientoInfinito.EliminarVehiculoEstacionado(vehiculo))
                    eliminado = true;

                return eliminado;
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
                }
            }*/
        }
    }
}
