using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servicio;
using Dominio;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        private VentaService ventaNegocio = new VentaService();
        private IList<DetalleVenta> listaDetalleVenta = new List<DetalleVenta>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //poblar grilla con coleccion de items detalleVenta.
            //listadetalleventa.add(new detalle venta)
            try
            {
                DetalleVenta aux = null;
                if (txtCantidad.Text != "")
                {
                    aux = new DetalleVenta();
                    aux.IdDetalleVenta = dgvDetalleVentas.RowCount + 1;
                    aux.IdVenta = ventaNegocio.contarVentas() + 1;
                    aux.Producto = (Producto)cboProductos.SelectedItem;
                    
                    //validamos que haya stock suficiente.
                    if (int.Parse(txtCantidad.Text) < aux.Producto.Stock)
                    {
                        aux.Cantidad = int.Parse(txtCantidad.Text);
                    }
                    else
                    {
                        MessageBox.Show("No hay suficiente stock Disponible.\n"
                            + "Ingrese una Cantidad Menor a: " + aux.Producto.Stock.ToString(), "Error de Stock");
                        return;
                    }
                    
                    aux.PrecioUnitario = aux.Producto.PrecioVenta;
                    aux.PrecioSubTotal = aux.PrecioUnitario * aux.Cantidad;
                    //antes de agregar el item verifico si no existe ya en el datagridview
                    
                    for(int x =0; x< dgvDetalleVentas.Rows.Count; x++)
                    {
                        if (dgvDetalleVentas.Rows[x].Cells[2].Value == aux.Producto)
                        {
                            MessageBox.Show("El Producto ya existe en el listado \n" + "Ingrese Otro Producto", "Error! Producto duplicado");
                            x = 0;
                            return;
                        }
                        
                    }
                    listaDetalleVenta.Add(aux);
                    
                    
                }
                else
                {
                    MessageBox.Show("Ingrese una cantidad", "Error! cantidad vacia");
                    return;
                }

                dgvDetalleVentas.DataSource = null;
                dgvDetalleVentas.DataSource = listaDetalleVenta;
                //actualizar el label con el total pesos del DGV
                int sum = 0;
                for (int i = 0; i < dgvDetalleVentas.Rows.Count; i++)
                {
                    sum += Convert.ToInt32(dgvDetalleVentas.Rows[i].Cells[5].Value);
                }
                lblTotalPesos.Text = "Ar$ " + sum.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        

        private void btnGenerarVenta_Click(object sender, EventArgs e)
        {
            //generar venta primero debera generar la venta y recuperar el id para poder dar de alta todos los
            //detalles de la venta.
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            ProductoService productoService = new ProductoService();
            ClienteService clienteService = new ClienteService();

            try
            {
                cboClientes.DataSource = clienteService.listarTodos();
                cboProductos.DataSource = productoService.listarTodos();

                dgvDetalleVentas.DataSource = listaDetalleVenta;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if(System.Text.RegularExpressions.Regex.IsMatch(txtCantidad.Text, "[^0-9]"))
            {
                MessageBox.Show("Solo numeros por favor.", "Error de ingreso");
                txtCantidad.Text = txtCantidad.Text.Remove(txtCantidad.Text.Length - 1);
            }
        }

        private void btnPruebas_Click(object sender, EventArgs e)
        {
            DetalleVenta aux = new DetalleVenta();
            //aux.Cantidad = int.Parse(txtCantidad.Text);
            //aux.IdDetalleVenta = dgvDetalleVentas.RowCount + 1;
            aux.IdVenta = ventaNegocio.contarVentas();

            MessageBox.Show(aux.IdVenta.ToString());


        }
    }
}
