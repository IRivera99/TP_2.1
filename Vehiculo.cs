using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_2._1
{
    class Vehiculo
    {
        public string Modelo { get; set; }
        public Persona Dueño { get; set; }
        public string Matricula { get; }
        public double Ancho { get; set; }
        public double Largo { get; set; }
        public TipoDimensiones Tamaño { get; }

        private static readonly Random randy = new Random();

        public Vehiculo()
        {
            Modelo = GenerarNombreAleatorio(5,0);
            Dueño = new Persona();
            Matricula = GenerarNombreAleatorio(3, 3);
            Ancho = randy.NextDouble() + randy.Next(0,3);
            Largo = randy.NextDouble() + randy.Next(0,5);
            Tamaño = SetTipoDimensiones(Ancho, Largo);
        }

        private TipoDimensiones SetTipoDimensiones(double ancho, double largo)
        {
            TipoDimensiones tipoDimensiones = TipoDimensiones.Standar;
            if (ancho < 4 && largo < 1.5)
                tipoDimensiones = TipoDimensiones.Mini;
            if (ancho > 5 || largo > 2)
                tipoDimensiones = TipoDimensiones.Max;
            return tipoDimensiones;
        }

        private string GenerarNombreAleatorio(int cantLetras, int cantNumeros)
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
