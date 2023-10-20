using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_2._1
{
    class Persona
    {
        public string Nombre { get; set; }
        public int Dni { get; set; }
        public TipoClientes TipoCliente { get; set; }

        private static readonly Random randy = new Random();
        private readonly Array tipoClientes = Enum.GetValues(typeof(TipoClientes));

        public Persona(bool aleatorio)
        {
            if (aleatorio)
            {
                Nombre = GenerarStringAleatorio(6, 0);
                Dni = int.Parse(GenerarStringAleatorio(0, 8));
                TipoCliente = DeterminarTipoCliente();
            }
            else
            {
                Nombre = string.Empty;
                Dni = 0;
                TipoCliente = TipoCliente = DeterminarTipoCliente();
            }
            
        }

        public Persona(string nombre)
        {
            Nombre = nombre;
            Dni = int.Parse(GenerarStringAleatorio(0, 8));
            TipoCliente = DeterminarTipoCliente();
        }

        private TipoClientes DeterminarTipoCliente()
        {
            int index = randy.Next(0, tipoClientes.Length);
            return (TipoClientes)tipoClientes.GetValue(index);
        }

        private string GenerarStringAleatorio(int cantLetras, int cantNumeros)
        {
            string nombre = "";
            string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numeros = "123456789";

            for (int i = 0; i < cantLetras; i++)
            {
                nombre += letras[randy.Next(0, letras.Length)];
            }

            for (int i = 0; i < cantNumeros; i++)
            {
                nombre += numeros[randy.Next(0, numeros.Length)];
            }

            return nombre;
        }

    }
}
