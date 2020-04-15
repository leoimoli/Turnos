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
    public class TurnosDao
    {
        private static MySql.Data.MySqlClient.MySqlConnection connection = new MySqlConnection(Properties.Settings.Default.db);
        public static bool ValidarTurnosYaExistentes(DateTime fecha, int idCentro)
        {
            bool YaExistenTurnos = false;
            connection.Close();
            connection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            DataTable Tabla = new DataTable();
            MySqlParameter[] oParam = {
                                      new MySqlParameter("Fecha_in", fecha),
             new MySqlParameter("idCentro_in", idCentro),};
            string proceso = "ValidarTurnosYaExistentes";
            MySqlDataAdapter dt = new MySqlDataAdapter(proceso, connection);
            dt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dt.SelectCommand.Parameters.AddRange(oParam);
            dt.Fill(Tabla);
            if (Tabla.Rows.Count > 0)
            {
                YaExistenTurnos = true;
            }
            connection.Close();
            return YaExistenTurnos;
        }
        public static bool GenerarTurnos(int horaDesde, int horaHasta, int totalDeTurnos, int idCentro, DateTime fecha, int FraccionDeMinutos)
        {
            bool Exito = false;
            int ContarInsert = 0;
            int idEstadoTurno = 1;
            int idPersona = Convert.ToInt32(null);
            int Hora = 0;
            int Minutos = 0;
            for (int i = 0; i < totalDeTurnos; i++)
            {
                if (ContarInsert == 0)
                {
                    Hora = horaDesde;
                    string HoraTurno = Convert.ToString(Hora);
                    connection.Close();
                    connection.Open();
                    string proceso = "AltaTurno";
                    MySqlCommand cmd = new MySqlCommand(proceso, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("fecha_in", fecha);
                    cmd.Parameters.AddWithValue("idCentro_in", idCentro);
                    cmd.Parameters.AddWithValue("idPersona_in", idPersona);
                    cmd.Parameters.AddWithValue("idEstadoTurno_in", idEstadoTurno);
                    cmd.Parameters.AddWithValue("HoraTurno_in", HoraTurno);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    ContarInsert = ContarInsert + 1;
                }
                else
                {
                    Minutos = Minutos + FraccionDeMinutos;
                    string HoraTurno = Convert.ToString(horaDesde);
                    string MinutosDeHora = Convert.ToString(Minutos);
                    HoraTurno = HoraTurno + ":" + Minutos;
                    connection.Close();
                    connection.Open();
                    string proceso = "AltaTurno";
                    MySqlCommand cmd = new MySqlCommand(proceso, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("fecha_in", fecha);
                    cmd.Parameters.AddWithValue("idCentro_in", idCentro);
                    cmd.Parameters.AddWithValue("idPersona_in", idPersona);
                    cmd.Parameters.AddWithValue("idEstadoTurno_in", idEstadoTurno);
                    cmd.Parameters.AddWithValue("HoraTurno_in", HoraTurno);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    ContarInsert = ContarInsert + 1;
                }
            }
            return Exito;
        }
    }
}
