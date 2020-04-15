using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Turnos.Dao;

namespace Turnos.Negocio
{
    public class TurnosNeg
    {
        public static bool GenerarTurnos(int horaDesde, int horaHasta, int totalDeTurnos, int idCentro, DateTime fecha, int FraccionDeMinutos)
        {
            bool exito = false;
            bool TurnosYaEsxistentes = false;
            try
            {
                TurnosYaEsxistentes = TurnosDao.ValidarTurnosYaExistentes(fecha, idCentro);
                if (TurnosYaEsxistentes == true)
                {
                    const string message = "Ya existen turnos generados en esa fecha para el centro de salud seleccionado.";
                    const string caption = "Error";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                               MessageBoxIcon.Exclamation);
                    throw new Exception();
                }
                else
                {
                    exito = TurnosDao.GenerarTurnos(horaDesde, horaHasta, totalDeTurnos, idCentro, fecha, FraccionDeMinutos);
                }
                
            }
            catch (Exception ex)
            { }
            return exito;
        }
    }
}
