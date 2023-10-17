using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_2._1
{
    class Estacionamiento
    {
        private List<Espacio> espacios;
        private bool infinito = true;

        public List<Espacio> Espacios
        {
            get { return espacios; }
        }       
        public bool Infinito
        {
            get { return infinito; }
        }

        public Estacionamiento()
        {
            espacios = new List<Espacio>();
            AgregarEspacio();
        }

        public Estacionamiento(int cantEspacios)
        {
            espacios = new List<Espacio>();
            infinito = false;
            for (int i = 0; i < cantEspacios; i++)
            {
                Espacio espacio = new Espacio();

                if (i == 2 || i == 6 || i == 11)
                    espacio.TipoCliente = TipoClientes.Vip;

                espacios.Add(espacio);
            }            
        }

        public bool EstacionarVehiculo(Vehiculo vehiculo)
        {
            Console.WriteLine(Espacios.Count);
            bool hayLugar = true;
            bool estacionado = false;
            int i = 0;

            while (hayLugar)
            {
                if (i == espacios.Count - 1)
                {
                    hayLugar = AgregarEspacio(); //Mientras sea infinito el estacionamiento siempre va a haber lugar
                }

                if (!espacios[i].Ocupado && 
                    (vehiculo.Dueño.TipoCliente == espacios[i].TipoCliente || vehiculo.Dueño.TipoCliente == TipoClientes.Regular) &&
                    (((int)vehiculo.Tamaño) <= ((int)espacios[i].TipoDimension)))
                {
                    Console.WriteLine("Se cumple");
                    espacios[i].OcuparLugar(vehiculo);
                    estacionado = true;
                    hayLugar = false; //No significa que no haya mas lugar, sino que corta el while porque ya lo estacionó
                }
                i++;
                //Console.WriteLine(i);
                if(i == 10000000)
                {
                    hayLugar = false;
                    Console.WriteLine("No se puede en el infinito (10000000 lugares)");
                }
            }
            return estacionado;
        }

        public bool AgregarEspacio()
        {
            bool agregado = false;
            if (Infinito)
            {
                Espacio espacio = new Espacio();
                espacios.Add(espacio);
                agregado = true;
            }
            return agregado;
        }
    }
}
