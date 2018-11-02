using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Servicio
{
    public class ProductoService
    {
        //Traer productos



        //Descontar Stock

        //Devolver Stock
        public IList<Producto> listarTodos()
        {
            IList<Producto> lista = new List<Producto>();
            AccesoDatos conexion = new AccesoDatos();

            try
            {
                conexion.setearConsulta("Select IdProducto, Nombre, Venta, Stock From Productos");
                conexion.leerConsulta();
                while (conexion.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.IdProducto = conexion.Lector.GetInt32(0);
                    aux.Nombre = conexion.Lector.GetString(1);
                    aux.PrecioVenta = conexion.Lector.GetDecimal(2);
                    aux.Stock = conexion.Lector.GetInt32(3);
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
                conexion = null;
            }
        }
    }
}
