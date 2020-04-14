using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turnos.Dao;
using Turnos.Entidades;

namespace Turnos.Negocio
{
    public class CentroDeSaludNeg
    {
        public static bool GuardaCentroDeSalud(CentroDeSalud _centro)
        {
            bool exito = false;
            try
            {
                ValidarDatos(_centro);
                //bool UsuarioExistente = ValidarUsuarioExistente(_cliente.NombreRazonSocial, _cliente.Cuit);
                //if (UsuarioExistente == true)
                //{
                //    const string message = "Ya existe un cliente registrado con el Nombre/Razón Social y Cuit ingresado.";
                //    const string caption = "Error";
                //    var result = MessageBox.Show(message, caption,
                //                                 MessageBoxButtons.OK,
                //                               MessageBoxIcon.Exclamation);
                //    throw new Exception();
                //}
                //else
                //{
                exito = CentroDeSaludDao.InsertCentroDeSalud(_centro);
                //}
            }
            catch (Exception ex)
            {

            }
            return exito;
        }

        private static void ValidarDatos(CentroDeSalud _centro)
        {
            throw new NotImplementedException();
        }

        public static List<CentroDeSalud> BuscarCentroDeSaludPorNombre(string nombreCentro)
        {
            List<CentroDeSalud> _listaCentros = new List<CentroDeSalud>();
            try
            {
                _listaCentros = CentroDeSaludDao.BuscarCentroDeSaludPorNombre(nombreCentro);
            }
            catch (Exception ex)
            {
            }
            return _listaCentros;
        }
    }
}
