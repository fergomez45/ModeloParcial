using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using ModeloParcial.Models;
using ModeloParcial.Repositories;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ModeloParcial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private IPeliculaRepository _repository;

        public PeliculasController(IPeliculaRepository repository)
        {
            _repository = repository;
        }

        //1) Recuperar todas las peliculas que estuvieron en estreno entre dos años recibidos como parámetro.

        //2) Modificar la base de datos y agregar las columnas: fechaBaja y motivo de baja como nulleables para registrar mediante Delete una baja lógica de la pelicula

        //3. Recuperar todas las películas de un deteminado género que estuvieron en estreno entre los años a y b


        // GET: api/<PeliculasController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            try
            {
                if(_repository.GetById(id) != null)
                    return Ok(_repository.GetById(id));
                else
                    return NotFound("Pelicula no encontrada.");
                
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }
        }

        // 1)
        [HttpGet("Entre")]
        public IActionResult GetByYears([FromQuery]int anioDesde, int anioHasta)
        {
            try
            {
                if(anioDesde < anioHasta)
                {
                    return Ok(_repository.GetAllByYears(anioDesde, anioHasta));
                }
                else
                {
                    return BadRequest("Periodo incorrecto.");
                }
                
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }
        }

        [HttpGet("Genero")]
        public IActionResult GetByGen([FromQuery]int idgenero, int anioDesde, int anioHasta)
        {
            try
            {
                if (anioDesde < anioHasta)
                {
                    return Ok(_repository.GetAllByGen(idgenero,anioDesde, anioHasta));
                }
                else
                {
                    return BadRequest("Periodo incorrecto.");
                }

            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }
        }

        // POST api/<PeliculasController>
        [HttpPost]
        public IActionResult Post([FromBody] Pelicula pelicula)
        {
            try
            {
                if (IsValid(pelicula))
                {
                    _repository.Create(pelicula);
                    return Ok("Pelicula registrada con exito.");
                }
                else
                {
                    return BadRequest("Debe completar todos los campos");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno.");
            }
            
        }

        private bool IsValid(Pelicula pelicula)
        {
            return !string.IsNullOrEmpty(pelicula.Titulo) && !string.IsNullOrEmpty(pelicula.Director) && pelicula.Anio != 0 && pelicula.IdGenero > 0;
        }

        // PUT api/<PeliculasController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            try
            {
                if (_repository.Update(id))
                {
                    return Ok("Pelicula fuera de cartelera.");
                }
                else
                {
                    return NotFound("Pelicula no encontrada.");
                }
                
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno.");
            }

        }

        [HttpDelete("baja/{id}")]
        public IActionResult Delete(int id, [FromBody] string motivoBaja)
        {

            try
            {
                if (_repository.Delete(id, motivoBaja))
                    return Ok("Se ha dado de baja la película correctamente");
                else
                    return NotFound($"La película con id {id} no existe");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }
    }
}
