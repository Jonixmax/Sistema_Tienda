using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sistema_Tienda
{
    public partial class Form1 : Form
    {
        private BindingList<Venta> listaVentas = new BindingList<Venta>();
        private List<Cliente> listaClientes = new List<Cliente>();
        private Producto[,] stockProductos = new Producto[5, 10];
        private string[] categorias = { "Electrónica", "Ropa", "Hogar", "Alimentos", "Otros" };
        private Dictionary<int, Venta> ventas = new Dictionary<int, Venta>(); 
        private int idVenta = 1; 
        public Form1()
        {
            
            InitializeComponent();
            InicializarCategoria();
            ConfigurarDataGridView();
            dataGridViewVentas.DataSource = listaVentas;
           
        }
        private void InicializarCategoria()
        {
            catego.Items.AddRange(categorias);
            catego.SelectedIndex = 0; // Seleccionar la primera categoría por defecto
        }

        // Configurar el DataGridView para que tenga 5 filas y 10 columnas
        private void ConfigurarDataGridView()
        {
            dataGridViewProductos.ColumnCount = 10; 
            dataGridViewProductos.RowCount = 5;     

            // Asignar los nombres de las categorías a las filas
            for (int i = 0; i < 5; i++)
            {
                dataGridViewProductos.Rows[i].HeaderCell.Value = categorias[i];
            }

           
        }

        private void Agregarproducto(object sender, EventArgs e)
        {
           try
            {
                int id = int.Parse(ID.Text);
                string nombre = Nombre.Text;
                string categoria = catego.SelectedItem.ToString();
                decimal precio = decimal.Parse(PRECIO.Text);
                int cantidad = int.Parse(stoc.Text);

                // Obtener el índice de la categoría
                int indiceCategoria = Array.IndexOf(categorias, categoria);

                // Buscar el primer espacio vacío en la fila correspondiente a la categoría
                bool productoAgregado = false;
                for (int i = 0; i < 10; i++)
                {
                    if (stockProductos[indiceCategoria, i] == null)
                    {
                        stockProductos[indiceCategoria, i] = new Producto(id, nombre, categoria, precio, cantidad);
                        productoAgregado = true;
                        break;
                    }
                }

                if (productoAgregado)
                {
                    StockGraficos();
                    ActualizarListaProductos(); 
                    LimpiarCampos();
                    MessageBox.Show("Producto registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProductoListBox(); 
                }
                else
                {
                    MessageBox.Show("No hay espacio disponible en esta categoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }



        private void ActualizarListaProductos()
        {
            dataGridViewProductos.Rows.Clear();
            ConfigurarDataGridView(); // Asegurar que las categorías sigan en su lugar

            for (int i = 0; i < 5; i++) // Recorrer filas (categorías)
            {
                for (int j = 0; j < 10; j++) // Recorrer columnas (productos)
                {
                    if (stockProductos[i, j] != null)
                    {
                        dataGridViewProductos.Rows[i].Cells[j].Value = stockProductos[i, j].Nombre; // Mostrar solo el nombre del producto
                    }
                }
            }
        }

        // Limpiar campos después de registrar
        private void LimpiarCampos()
        {
            ID.Clear();
            Nombre.Clear();
            catego.SelectedIndex = 0;
            PRECIO.Clear();
            stoc.Clear();
        }

        private void AgregarCliente(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtClienteID.Text);
                string nombre = txtClienteNombre.Text;
                string direccion = txtClienteDireccion.Text;
                string telefono = txtClienteTelefono.Text;

                // Verificar que el ID no esté repetido
                if (listaClientes.Exists(c => c.ID == id))
                {
                    MessageBox.Show("El ID ya está registrado. Use otro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Crear el nuevo cliente y agregarlo a la lista
                Cliente nuevoCliente = new Cliente(id, nombre, direccion, telefono);
                listaClientes.Add(nuevoCliente);

                // Actualizar la visualización
                ActualizarListaClientes();
                LimpiarCamposCliente();
                CargarClientesComboBox();
                MessageBox.Show("Cliente registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }



        private void ActualizarListaClientes()
        {
            listBoxClientes.Items.Clear();
            foreach (var cliente in listaClientes)
            {
                listBoxClientes.Items.Add($"ID: {cliente.ID} -Nombre: {cliente.Nombre}-Telefono: {cliente.Telefono} -Direccion: {cliente.Direccion}");
            }
        }

        // Método para limpiar los campos después de registrar un cliente
        private void LimpiarCamposCliente()
        {
            txtClienteID.Clear();
            txtClienteNombre.Clear();
            txtClienteTelefono.Clear();
            txtClienteDireccion.Clear();

        }

        private void RealizarVenta(object sender, EventArgs e)
        {
            try
            {
                // Validaciones previas
                if (comboBoxClientes.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (comboBoxProductos.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int cantidad = (int)numericUpDownCantidad.Value;
                if (cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener cliente y producto seleccionados
                Cliente clienteSeleccionado = (Cliente)comboBoxClientes.SelectedItem;
                Producto productoSeleccionado = (Producto)comboBoxProductos.SelectedItem;

                // Verificar stock suficiente
                if (productoSeleccionado.Cantidad < cantidad)
                {
                    MessageBox.Show("No hay suficiente stock disponible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Registrar la venta
                decimal total = cantidad * productoSeleccionado.Precio;
                Venta nuevaVenta = new Venta(idVenta, clienteSeleccionado, productoSeleccionado, cantidad, total);
                ventas.Add(idVenta, nuevaVenta);

                // Actualizar stock del producto
                productoSeleccionado.Cantidad -= cantidad;

                
                ActualizarListaVentas();
                StockGraficos();
                CargarProductoListBox();

                // Incrementar ID de venta
                idVenta++;

                MessageBox.Show("Venta realizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }


        private void ActualizarListaVentas()
        {
            dataGridViewVentas.DataSource = null;
            dataGridViewVentas.DataSource = ventas.Values.Select(v => new
            {
                ID = v.ID,
                Cliente = v.Cliente.Nombre,  
                Producto = v.Producto.Nombre, 
                Cantidad = v.Cantidad,
                Total = v.Total
            }).ToList();
        }

        // Método para cargar clientes en el ComboBox
        private void CargarClientesComboBox()
        {
            comboBoxClientes.DataSource = null;
            comboBoxClientes.DataSource = listaClientes;
            comboBoxClientes.DisplayMember = "Nombre";
        }

        private void CargarProductoListBox()
        {
            comboBoxProductos.Items.Clear(); 

            for (int i = 0; i < 5; i++) // 5 categorías
            {
                for (int j = 0; j < 10; j++) // 10 productos por categoría
                {
                    if (stockProductos[i, j] != null) 
                    {
                        comboBoxProductos.Items.Add(stockProductos[i, j]); 
                    }
                }
            }

            comboBoxProductos.DisplayMember = "NCP"; // Mostrar nombre , stock y precio

        }

        private void Eliminar(object sender, EventArgs e)

        {


            if (dataGridViewVentas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona una venta para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el índice de la fila seleccionada
            int indiceFila = dataGridViewVentas.SelectedRows[0].Index;
            int idVenta = Convert.ToInt32(dataGridViewVentas.Rows[indiceFila].Cells[0].Value);

            // Verificar si la venta existe en el diccionario
            if (ventas.ContainsKey(idVenta))
            {
                // Obtener la venta y devolver productos al stock
                Venta venta = ventas[idVenta];
                venta.Producto.Cantidad += venta.Cantidad;

                // Eliminar la venta del diccionario y la lista
                ventas.Remove(idVenta);
                listaVentas.Remove(venta);

                // Actualizar la interfaz
                ActualizarListaProductos();
                ActualizarListaVentas();
                CargarProductoListBox();
                StockGraficos();
                MessageBox.Show("Venta eliminada y productos devueltos al stock.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se encontró la venta en el sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }


        private void StockGraficos()
        {
            chartStock.Series.Clear(); 

            Series serie = new Series("Stock Disponible")
            {
                ChartType = SeriesChartType.Column, 
                IsValueShownAsLabel = true 
            };

            string[] categorias = { "Electrónica", "Ropa", "Hogar", "Alimentos", "Otros" };

            // Recorrer la matriz y calcular el stock total por categoría
            for (int i = 0; i < 5; i++)
            {
                int totalCategoria = 0;

                for (int j = 0; j < 10; j++)
                {
                    if (stockProductos[i, j] != null) 
                    {
                        totalCategoria += stockProductos[i, j].Cantidad; 
                    }
                }

                
                serie.Points.AddXY(categorias[i], totalCategoria);
            }

            chartStock.Series.Add(serie); 
            chartStock.ChartAreas[0].RecalculateAxesScale();
            chartStock.Invalidate(); 
        }

      
    }
}



