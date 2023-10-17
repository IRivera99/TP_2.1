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

            for (int i = 0; i < 15; i++)
            {
                vehiculos.Add(new Vehiculo());
            }

            foreach (Vehiculo vehiculo in vehiculos)
            {
                if (!estacionamientoFinito.EstacionarVehiculo(vehiculo))
                {
                    estacionamientoInfinito.EstacionarVehiculo(vehiculo);
                }
            }       

            Console.ReadLine();
        }
    }
}
