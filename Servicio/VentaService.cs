using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;


namespace Servicio
{
    public class VentaService
    {
        public int contarVentas()
        {
            int conteo = 0;
            AccesoDatos conexion = null;
            try
            {
                conexion = new AccesoDatos();
                conexion.setearConsulta("select count(IdVenta) from VENTAS ");
                conexion.abrirConexion();
                conteo = conexion.ejecutarAccionReturn();
                return conteo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if(conexion != null)
                {
                    conexion.cerrarConexion();
                }
            }


            
        }
        //crear venta

        //anular venta

        
    }
}
