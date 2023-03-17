using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Herramientas
{
    public static class Utils
    {

        public static Turno GetTurnoActual(List<Turno> turnos)
        {
            var horaActual = DateTime.Now;
            foreach(var turno in turnos)
    {
                if (horaActual.TimeOfDay >= turno.HoraInicio.TimeOfDay && horaActual.TimeOfDay < turno.HoraFin.TimeOfDay)
                {
                    return turno;
                }
            }
            return null;
        }

        public static bool JornadaPerteneceAlTurnoActual(List<Turno> turnos, JornadaLaboral? jornadaLaboral)
        {
            var turnoActual = GetTurnoActual(turnos);

            if (jornadaLaboral != null && turnoActual != null)
            {
                // Verifica si la hora de inicio de la jornada laboral está dentro del rango de horas del turno actual
                if (jornadaLaboral.FechaInicio.TimeOfDay >= turnoActual.HoraInicio.TimeOfDay && jornadaLaboral.FechaInicio.TimeOfDay <= turnoActual.HoraFin.TimeOfDay)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<JornadaLaboral> SPGetJornadas(int id)
        {
            string connectionString = "Server=.;Database=ControlCalidad;Trusted_Connection=True;TrustServerCertificate = True;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("ObtenerJornadas", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter("@opid", SqlDbType.Int);
            parameter.Value = id;
            command.Parameters.Add(parameter);

            SqlDataReader reader = command.ExecuteReader();

            List<JornadaLaboral> jornadas = new List<JornadaLaboral>();
            while (reader.Read())
            {
                JornadaLaboral jornada = new JornadaLaboral();
                jornada.Id = Convert.ToInt32(reader["Id"]);
                jornada.FechaInicio = DateTime.ParseExact(reader["FechaInicio"].ToString(), "dd/M/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                jornadas.Add(jornada);
            }
            return jornadas;

            reader.Close();
            connection.Close();
        }
    }
}
