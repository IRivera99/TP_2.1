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
            bool hayLugar = true;
            bool estacionado = false;
            int i = 0;

            while (hayLugar)
            {
                if (i == espacios.Count - 1)
                    hayLugar = AgregarEspacio(); //Mientras sea infinito el estacionamiento siempre va a haber lugar

                if (!espacios[i].Ocupado && 
                    ((espacios[i].TipoCliente ==  vehiculo.Dueño.TipoCliente) || vehiculo.Dueño.TipoCliente == TipoClientes.Vip) &&
                    (((int)vehiculo.Tamaño) <= ((int)espacios[i].TipoDimension)))
                {
                    espacios[i].OcuparLugar(vehiculo);
                    estacionado = true;
                    hayLugar = false; //No significa que no haya mas lugar, sino que corta el while porque ya lo estacionó
                }

                i++;
            }

            return estacionado;
        }

        private bool AgregarEspacio()
        {
            bool agregado = false;
            if (Infinito)
            {
                espacios.Add(new Espacio());
                agregado = true;
            }
            return agregado;
        }



    }
}
