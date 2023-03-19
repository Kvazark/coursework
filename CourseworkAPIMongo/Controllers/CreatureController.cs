

using System.Collections.Generic;
using System.Threading.Tasks;
using CourseworkAPIMongo.Models;
using CourseworkAPIMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkAPIMongo.Controllers
{
    [ApiController]
    [Route("creatures")]
        public class CreaturesController : ControllerBase
        {
            private readonly CreatureService _creatureService;

            public CreaturesController(CreatureService creatureService) =>
                _creatureService = creatureService;

            [HttpGet]
            public async Task<List<Creature>> Get() =>
                await _creatureService.GetAsync();

            [HttpGet("{id:length(24)}")]
            public async Task<ActionResult<Creature>> Get(string id)
            {
                var creature = await _creatureService.GetAsync(id);

                if (creature is null)
                {
                    return NotFound();
                }

                return creature;
            }

            [HttpPost]
            public async Task<IActionResult> Post(Creature newCreature)
            {
                await _creatureService.CreateAsync(newCreature);

                return CreatedAtAction(nameof(Get), new { id = newCreature.Id }, newCreature);
            }

            [HttpPut("{id:length(24)}")]
            public async Task<IActionResult> Update(string id, Creature updatedCreature)
            {
                var creature = await _creatureService.GetAsync(id);

                if (creature is null)
                {
                    return NotFound();
                }

                updatedCreature.Id = creature.Id;

                await _creatureService.UpdateAsync(id, updatedCreature);

                return NoContent();
            }

            [HttpDelete("{id:length(24)}")]
            public async Task<IActionResult> Delete(string id)
            {
                var creature = await _creatureService.GetAsync(id);

                if (creature is null)
                {
                    return NotFound();
                }

                await _creatureService.RemoveAsync(id);

                return NoContent();
            }
        }
    }
