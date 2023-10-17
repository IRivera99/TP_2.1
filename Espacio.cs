using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_2._1
{
    class Espacio
    {
        public TipoClientes TipoCliente { get; set; }
        public TipoDimensiones TipoDimension { get; }

        private Vehiculo vehiculo;
        public Vehiculo Vehiculo
        {
            get { return vehiculo; }
        }

        private bool ocupado = false;
        public bool Ocupado
        {
            get { return ocupado; }
        }

        private static readonly Random randy = new Random();
        private readonly Array tipoDimensiones = Enum.GetValues(typeof(TipoDimensiones));

        public Espacio()
        {
            TipoCliente = TipoClientes.Regular;
            TipoDimension = DeterminarTipoDimension();
        }

        public void OcuparLugar(Vehiculo vehiculo)
        {
            if (!Ocupado)
            {
                this.vehiculo = vehiculo;
                ocupado = true;
            }                
        }

        private TipoDimensiones DeterminarTipoDimension()
        {
            int index = randy.Next(0, tipoDimensiones.Length);
            return (TipoDimensiones)tipoDimensiones.GetValue(index);
        }
    }
}
