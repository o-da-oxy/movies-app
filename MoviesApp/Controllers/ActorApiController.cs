using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Services.ActorServices;
using MoviesApp.Services.Dto;

namespace MoviesApp.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorApiController: ControllerBase
    {
        private readonly IActorService _service;

        public ActorApiController(IActorService service)
        {
            _service = service;
        }
        
        [HttpGet] // GET: /api/actors
        //[ProducesResponseType(200, Type = typeof(IEnumerable<ActorDto>))]  
        //[ProducesResponseType(404)]
        public ActionResult<IEnumerable<ActorDto>> GetActors() //тип возвр значения конкретный!
        {
            return Ok(_service.GetAllActors());
        }
        
        [HttpGet("{id}")] // GET: /api/actors/5
        //[ProducesResponseType(200, Type = typeof(ActorDto))]  
        //[ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var actor = _service.GetActor(id);
            if (actor == null) return NotFound();  
            return Ok(actor);
        }
        
        [HttpPost] // POST: api/actors
        public ActionResult<ActorDto> PostActor(ActorDto inputDto)
        {
            var movie = _service.AddActor(inputDto);
            return CreatedAtAction("GetById", new { id = movie.Id }, movie);
        }
        
        [HttpPut("{id}")] // PUT: api/actors/5
        public IActionResult UpdateActor(int id, ActorDto editDto)
        {
            var movie = _service.UpdateActor(editDto);

            if (movie==null)
            {
                return BadRequest();
            }

            return Ok(movie);
        }
        
        [HttpDelete("{id}")] // DELETE: api/actor/5
        public ActionResult<ActorDto> DeleteActor(int id)
        {
            var movie = _service.DeleteActor(id);
            if (movie == null) return NotFound();  
            return Ok(movie);
        }
    }
}