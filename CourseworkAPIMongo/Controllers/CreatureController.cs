

using System.Collections.Generic;
using CourseworkAPIMongo.Models;
using CourseworkAPIMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkAPIMongo.Controllers
{
    public class CreatureController
    {
        [Route("api/[controller]")]
        [ApiController]
        public class CreaturesController : ControllerBase
        {
            private readonly CreatureService _creatureService;
        
            public CreaturesController(CreatureService creatureService)
            {
                _creatureService = creatureService;
            }
        
            [HttpGet]
            public ActionResult<List<Creature>> Get() =>
                _creatureService.Get();
        
            [HttpGet("{id:length(24)}", Name = "GetCreature")]
            public ActionResult<Creature> Get(string id)
            {
                var creature = _creatureService.Get(id);
        
                if (creature == null)
                {
                    return NotFound();
                }
        
                return creature;
            }
        
            [HttpPost]
            public ActionResult<Creature> Create(Creature creature)
            {
                _creatureService.Create(creature);
        
                return CreatedAtRoute("GetCreature", new { id = creature.Id.ToString() }, creature);
            }
        
            [HttpPut("{id:length(24)}")]
            public IActionResult Update(string id, Creature creatureIn)
            {
                var creature = _creatureService.Get(id);
        
                if (creature == null)
                {
                    return NotFound();
                }
        
                _creatureService.Update(id, creatureIn);
        
                return NoContent();
            }
        
            [HttpDelete("{id:length(24)}")]
            public IActionResult Delete(string id)
            {
                var creature = _creatureService.Get(id);
        
                if (creature == null)
                {
                    return NotFound();
                }
        
                _creatureService.Remove(id);
        
                return NoContent();
            }
        }
    }
}