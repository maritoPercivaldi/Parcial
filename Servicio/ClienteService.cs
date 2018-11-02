using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Servicio
{
    public class ClienteService
    {
        //Traer Clientes
        public IList<Cliente> listarTodos()
        {
            IList<Cliente> lista = new List<Cliente>();
            AccesoDatos conexion = new AccesoDatos();

            try
            {
                conexion.setearConsulta("Select IdCliente, Nombre From Clientes");
                conexion.leerConsulta();
                while (conexion.Lector.Read())
                {
                    Cliente aux = new Cliente(conexion.Lector.GetInt32(0), conexion.Lector.GetString(1));
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
