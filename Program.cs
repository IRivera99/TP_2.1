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
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            Estacionamiento estacionamientoFinito = new Estacionamiento(12);
            Estacionamiento estacionamientoInfinito = new Estacionamiento();

            int estaFinito = 0;
            int estaInfi = 0;

            for (int i = 0; i < 5; i++)
            {
                vehiculos.Add(new Vehiculo());
            }

            foreach (Vehiculo vehiculo in vehiculos)
            {
                if (!estacionamientoFinito.EstacionarVehiculo(vehiculo))
                {
                    Console.WriteLine("No se pudo en el finito");
                    estacionamientoInfinito.EstacionarVehiculo(vehiculo);
                }
            }

            foreach (Espacio espacio in estacionamientoFinito.Espacios)
            {
                if (espacio.Ocupado)
                    estaFinito++;
            }

            foreach (Espacio espacio in estacionamientoInfinito.Espacios)
            {
                if (espacio.Ocupado)
                    estaInfi++;
            }

            Console.WriteLine(estaFinito);
            Console.WriteLine(estaInfi);
            Console.WriteLine(estacionamientoFinito.Espacios.Count);
            Console.WriteLine(estacionamientoInfinito.Espacios.Count);


            Console.ReadLine();
        }
    }
}
