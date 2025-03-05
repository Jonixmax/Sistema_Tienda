using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Tienda
{
    internal class Cliente
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public Cliente(int id, string nombre, string direccion, string telefono)
        {
            ID = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }
    }
}

