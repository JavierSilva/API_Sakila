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
        ActorBD actorBD;
        public ActorController(ActorBD pActorBD)
        {
            this.actorBD = pActorBD;
        }

        [HttpGet("RetornarActores")]
        public IActionResult Get()
        {
            try
            {
                return Ok(actorBD.RetornarTodosLosActores());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + ". Cadena de conexión: " + Environment.GetEnvironmentVariable("CADENA_CONEXION"));
            }
        }

        [HttpGet("RetornarActores/{pId}")]
        public IActionResult Get(int pId)
        {
            try
            {
                return Ok(actorBD.RetornarActor(pId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + ". Cadena de conexión: " + Environment.GetEnvironmentVariable("CADENA_CONEXION"));
            }
        }
        [HttpPost("GuardarActor")]
        public IActionResult Post(Actor pActor)
        {
            try
            {
                actorBD.GuardarActor(pActor);

                return Ok("Registro guardado!!!");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
