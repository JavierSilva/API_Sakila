using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Sakila.Modelos
{
    public class Actor
    {
        public int Id
        {
            get;
            set;
        }

        public string Nombres
        {
            get;
            set;
        }

        public string Apellidos
        {
            get;
            set;
        }

        public DateTime FechaUltimaModificacion
        {
            get;            
        }

        public Actor()
        {
            Id = 0;
            Nombres = string.Empty;
            Apellidos = string.Empty;
            FechaUltimaModificacion = DateTime.Today;
        }

        public Actor(string pNombres, string pApellidos)
        {
            Id = 0;
            Nombres = pNombres;
            Apellidos = pApellidos;
            FechaUltimaModificacion = DateTime.Today;
        }

        public Actor(int pId, string pNombres, string pApellidos, DateTime pFechaModificacion)
        {
            Id = pId;
            Nombres = pNombres;
            Apellidos = pApellidos;
            FechaUltimaModificacion = DateTime.Today;
        }
    }
}
