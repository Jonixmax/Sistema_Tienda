using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Tienda
{
    public class Producto
    {
        // Propiedades de la clase Producto
        public int ID { get; set; } 
        public string Nombre { get; set; } 
        public string Categoria { get; set; } 
        public decimal Precio { get; set; } 
        public int Stock { get; set; } 

        // Constructor de la clase Producto
        public Producto(int id, string nombre, string categoria, decimal precio, int stock)
        {
            ID = id;
            Nombre = nombre; 
            Categoria = categoria; 
            Precio = precio; 
            Stock = stock; 
        }

        // Propiedad que retorna una cadena con el nombre del producto, la cantidad y el precio
        public string NCP
        {
            get { return $"{Nombre} - Stock: {Stock} - Precio: ${Precio}"; }
        }
    }
}