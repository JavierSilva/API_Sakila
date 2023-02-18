using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Sakila.Data;
using API_Sakila.Modelos;

namespace API_Sakila.Controllers
{
    
    [ApiController]
    public class ActorController : ControllerBase
    {
        [HttpGet("RetornarActores")]
        public IList<Actor> Get()
        {
            try
            {
                return new ActorBD().RetornarTodosLosActores();
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("RetornarActores/{pId}")]
        public Actor Get(int pId)
        {
            try
            {
                return new ActorBD().RetornarActor(pId);
            }
            catch
            {
                return null;
            }
        }
        [HttpPost("GuardarActor")]
        public string Post(Actor pActor)
        {
            try
            {
                ActorBD actorBD = new ActorBD();

                actorBD.GuardarActor(pActor);

                return "Registro guardado!!!";
            }
            catch
            {
                return "Error";
            }
        }
    }
}
