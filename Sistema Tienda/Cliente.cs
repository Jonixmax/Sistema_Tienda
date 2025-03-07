using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Tienda
{
    internal class Cliente
    {
        // Propiedades de la clase Cliente
        public int ID { get; set; } 
        public string Nombre { get; set; } 
        public string Direccion { get; set; } 
        public string Telefono { get; set; } 

        // Constructor de la clase Cliente
        public Cliente(int id, string nombre, string direccion, string telefono)
        {
            ID = id; 
            Nombre = nombre; 
            Direccion = direccion; 
            Telefono = telefono;
        }
    }
}
