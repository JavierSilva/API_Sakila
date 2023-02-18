using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using API_Sakila.Modelos;

namespace API_Sakila.Data
{
    public class ActorBD
    {
        private MySqlConnection cnx;
        private MySqlCommand cmd;

        public ActorBD()
        {
            cnx = new MySqlConnection();
            cmd = new MySqlCommand();

            cmd.Connection = cnx;
        }

        private void AbrirConexion()
        {
            try
            {
                cnx.ConnectionString = "server=localhost;user id=manager;password=Pa$$w0rd;port=3306;database=sakila";

                cnx.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CerrarConexion()
        {
            try
            {
                cnx.Close();
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
                AbrirConexion();

                cmd.CommandType = System.Data.CommandType.Text;

                if (pActor.Id == 0)
                    cmd.CommandText = "INSERT INTO actor(first_name, last_name) VALUES(@pNombres, @pApellidos)";
                else
                    cmd.CommandText = "UPDATE actor SET first_name = @pNombres, last_name = @pApellidos, last_update = @pFechaModificacion WHERE actor_id = @pIdActor";

                cmd.Parameters.AddWithValue("@pIdActor", pActor.Id);
                cmd.Parameters.AddWithValue("@pNombres", pActor.Nombres);
                cmd.Parameters.AddWithValue("@pApellidos", pActor.Apellidos);
                cmd.Parameters.AddWithValue("@pFechaModificacion", pActor.FechaUltimaModificacion);

                cmd.ExecuteNonQuery();

                CerrarConexion();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Actor RetornarActor(int pId)
        {
            try
            {
                AbrirConexion();

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
                
                CerrarConexion();

                return actor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Actor> RetornarTodosLosActores()
        {
            try
            {
                IList<Actor> listaDeActores = new List<Actor>();

                AbrirConexion();

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "SELECT actor_id, first_name, last_name, last_update FROM actor";

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                    listaDeActores.Add(new Actor(Convert.ToInt32(dr["actor_id"]), dr["first_name"].ToString(), dr["last_name"].ToString(), Convert.ToDateTime(dr["last_update"])));

                dr.Close();

                CerrarConexion();

                return listaDeActores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
