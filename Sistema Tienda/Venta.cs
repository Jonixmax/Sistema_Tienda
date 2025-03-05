using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Tienda
{
    internal class Venta
    {

        public int ID { get; set; }
        public Cliente Cliente { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public Venta(int id, Cliente cliente, Producto producto, int cantidad, decimal total)
        {
            ID = id;
            Cliente = cliente;
            Producto = producto;
            Cantidad = cantidad;
            Total = total;
        }


    }


}

