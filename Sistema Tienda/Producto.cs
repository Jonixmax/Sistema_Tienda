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
        public int ID { get; set; } // Identificador único del producto
        public string Nombre { get; set; } // Nombre del producto
        public string Categoria { get; set; } // Categoría a la que pertenece el producto
        public decimal Precio { get; set; } // Precio del producto
        public int Cantidad { get; set; } // Cantidad disponible en el inventario

        // Constructor de la clase Producto
        public Producto(int id, string nombre, string categoria, decimal precio, int cantidad)
        {
            ID = id; // Inicializa el ID del producto
            Nombre = nombre; // Inicializa el nombre del producto
            Categoria = categoria; // Inicializa la categoría del producto
            Precio = precio; // Inicializa el precio del producto
            Cantidad = cantidad; // Inicializa la cantidad disponible del producto
        }

        // Propiedad que retorna una cadena con el nombre del producto, la cantidad y el precio
        public string NCP
        {
            get { return $"{Nombre} - Stock: {Cantidad} - Precio: ${Precio}"; }
        }
    }
}