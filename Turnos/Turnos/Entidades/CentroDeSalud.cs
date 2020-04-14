using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnos.Entidades
{
  public class CentroDeSalud
    {
        public int idCentro { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string HorarioDesde { get; set; }
        public string Horariohasta { get; set; }
        public int CantidadDeTurnos { get; set; }
        public int FraccionTurnos { get; set; }
        public int idUsuario { get; set; }
    }
}
