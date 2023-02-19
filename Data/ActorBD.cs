using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using API_Sakila.Modelos;
using Microsoft.AspNetCore.Components.Web;

namespace API_Sakila.Data
{
    public class ActorBD
    {
        private MySqlConnection cnx;

        bool inicializado = false;

        public ActorBD()
        {
            cnx = new MySqlConnection();

            cnx.ConnectionString = Environment.GetEnvironmentVariable("CADENA_CONEXION");
        }

        private void Inicializar()
        {
            try
            {
                if (this.inicializado)
                    return;


                cnx.Open();

                try
                {

                    var cmd = new MySqlCommand();

                    cmd.Connection = cnx;

                    cmd.CommandText = @"CREATE TABLE `actor` (
  `actor_id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `first_name` varchar(45) NOT NULL,
  `last_name` varchar(45) NOT NULL,
  `last_update` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`actor_id`),
  KEY `idx_actor_last_name` (`last_name`)
) ENGINE=InnoDB AUTO_INCREMENT=202 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci";

                    cmd.ExecuteNonQuery();

                    inicializado = true;

                    this.cnx.Close();
                }
                catch (Exception)
                {
                    inicializado = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GuardarActor(Actor pActor)
        {
            try
            {
                Inicializar();

                if (cnx.State != System.Data.ConnectionState.Open)
                    cnx.Open();

                var cmd = new MySqlCommand();

                cmd.Connection = cnx;
                cmd.CommandType = System.Data.CommandType.Text;

                if (pActor.Id == 0)
                {
                    cmd.CommandText = "INSERT INTO actor(first_name, last_name) VALUES(@pNombres, @pApellidos)";

                    cmd.Parameters.AddWithValue("@pNombres", pActor.Nombres);
                    cmd.Parameters.AddWithValue("@pApellidos", pActor.Apellidos);
                }                   
                else
                {
                    cmd.CommandText = "UPDATE actor SET first_name = @pNombres, last_name = @pApellidos, last_update = @pFechaModificacion WHERE actor_id = @pIdActor";

                    cmd.Parameters.AddWithValue("@pIdActor", pActor.Id);
                    cmd.Parameters.AddWithValue("@pNombres", pActor.Nombres);
                    cmd.Parameters.AddWithValue("@pApellidos", pActor.Apellidos);
                    cmd.Parameters.AddWithValue("@pFechaModificacion", pActor.FechaUltimaModificacion);
                }

                cmd.ExecuteNonQuery();
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.cnx.Close();
            }
        }

        public Actor RetornarActor(int pId)
        {
            try
            {
                Inicializar();

                if (cnx.State != System.Data.ConnectionState.Open)
                    cnx.Open();

                var cmd = new MySqlCommand();

                cmd.Connection = cnx;

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "SELECT actor_id, first_name, last_name, last_update FROM actor WHERE actor_id = @pActorId";

                cmd.Parameters.AddWithValue("@pActorId", pId);

                MySqlDataReader dr = cmd.ExecuteReader();

                Actor actor = null;


                if (dr.HasRows)
                {
                    dr.Read();

                    actor = new Actor(Convert.ToInt32(dr["actor_id"]), dr["first_name"].ToString(), dr["last_name"].ToString(), Convert.ToDateTime(dr["last_update"]));

                    dr.Close();
                }                

                return actor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.cnx.Close();
            }
        }

        public IList<Actor> RetornarTodosLosActores()
        {
            try
            {
                Inicializar();

                if (cnx.State != System.Data.ConnectionState.Open)
                    cnx.Open();

                IList<Actor> listaDeActores = new List<Actor>();

                var cmd = new MySqlCommand();

                cmd.Connection = cnx;

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "SELECT actor_id, first_name, last_name, last_update FROM actor";

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                    listaDeActores.Add(new Actor(Convert.ToInt32(dr["actor_id"]), dr["first_name"].ToString(), dr["last_name"].ToString(), Convert.ToDateTime(dr["last_update"])));

                dr.Close();

                return listaDeActores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.cnx.Close();
            }
        }
    }
}
