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
            string Fec = fecha.ToShortDateString();
            fecha = Convert.ToDateTime(Fec);
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
            string HoraTurno = "";
            string MinutosDeHora = "";

            string Fec = fecha.ToShortDateString();
            fecha = Convert.ToDateTime(Fec);
            for (int i = 0; i < totalDeTurnos; i++)
            {
                if (ContarInsert == 0)
                {
                    Hora = horaDesde;
                    HoraTurno = Convert.ToString(Hora);
                    connection.Close();
                    connection.Open();
                    if (Minutos == 0)
                    {
                        HoraTurno = HoraTurno + ":" + "00";
                    }

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
                    HoraTurno = Convert.ToString(Hora);
                    MinutosDeHora = Convert.ToString(Minutos);
                    if (Minutos > 60)
                    {
                        Hora = Hora + 1;
                        HoraTurno = Convert.ToString(Hora);
                        Minutos = Minutos - 60;
                    }
                    if (Minutos == 60)
                    {
                        Hora = Hora + 1;
                        HoraTurno = Convert.ToString(Hora);
                        Minutos = 0;
                    }
                    if (Minutos == 0)
                    {
                        MinutosDeHora = "00";
                    }
                    HoraTurno = HoraTurno + ":" + MinutosDeHora;
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
                Exito = true;
            }
            return Exito;
        }
    }
}
