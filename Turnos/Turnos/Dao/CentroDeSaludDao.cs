using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turnos.Entidades;

namespace Turnos.Dao
{
    public class CentroDeSaludDao
    {
        private static MySql.Data.MySqlClient.MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.db);
        public static bool InsertCentroDeSalud(CentroDeSalud _centro)
        {
            bool exito = false;
            connection.Close();
            connection.Open();
            string proceso = "AltaCentro";
            MySqlCommand cmd = new MySqlCommand(proceso, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Nombre_in", _centro.Nombre);
            cmd.Parameters.AddWithValue("Domicilio_in", _centro.Domicilio);
            cmd.Parameters.AddWithValue("Telefono_in", _centro.Telefono);
            cmd.Parameters.AddWithValue("HorarioDesde_in", _centro.HorarioDesde);
            cmd.Parameters.AddWithValue("Horariohasta_in", _centro.Horariohasta);
            cmd.Parameters.AddWithValue("CantidadDeTurnos_in", _centro.CantidadDeTurnos);
            cmd.Parameters.AddWithValue("FraccionTurnos_in", _centro.FraccionTurnos);
            cmd.Parameters.AddWithValue("idUsuario_in", _centro.idUsuario);
            cmd.ExecuteNonQuery();
            exito = true;
            connection.Close();
            return exito;
        }
        public static List<string> CargarComboCentroDeSalud()
        {
            connection.Close();
            connection.Open();
            List<string> _listaProvincia = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = { };
            string proceso = "ListarCentrosDeSalud";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    _listaProvincia.Add(item["idCentro"].ToString() + "," + item["Nombre"].ToString());
                }
            }
            connection.Close();
            return _listaProvincia;
        }
        public static List<CentroDeSalud> BuscarCentroDeSaludPorNombre(string nombreCentro)
        {
            connection.Close();
            connection.Open();
            List<Entidades.CentroDeSalud> lista = new List<Entidades.CentroDeSalud>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = {
                                      new MySqlParameter("Nombre_in", nombreCentro)};
            string proceso = "BuscarCentroDeSaludPorNombre";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                foreach (DataRow item in Tabla.Rows)
                {
                    CentroDeSalud listaCentro = new CentroDeSalud();
                    listaCentro.idCentro = Convert.ToInt32(item["idCentro"].ToString());
                    listaCentro.Nombre = item["Nombre"].ToString();
                    listaCentro.Domicilio = item["Domicilio"].ToString();
                    listaCentro.Telefono = item["Telefono"].ToString();
                    listaCentro.HorarioDesde = item["HoraDesde"].ToString();
                    listaCentro.Horariohasta = item["HoraHasta"].ToString();
                    listaCentro.CantidadDeTurnos = Convert.ToInt32(item["TotalTurnos"].ToString());
                    listaCentro.FraccionTurnos = Convert.ToInt32(item["FraccionTurnoPorHora"].ToString());
                    lista.Add(listaCentro);
                }
            }
            connection.Close();
            return lista;
        }
    }
}
