using System.Collections.Generic;
using System.Threading.Tasks;
using CourseworkAPIMongo.Models;
using CourseworkAPIMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkAPIMongo.Controllers
{

        [ApiController]
        [Route("professors")]
        public class ProfessorsController : ControllerBase
        {
            private readonly ProfessorService _professorService;

            public ProfessorsController(ProfessorService professorService) =>
                _professorService = professorService;

            [HttpGet]
            public async Task<List<Professor>> Get() =>
                await _professorService.GetAsync();

            [HttpGet("{id:length(24)}")]
            public async Task<ActionResult<Professor>> Get(string id)
            {
                var professor = await _professorService.GetAsync(id);

                if (professor is null)
                {
                    return NotFound();
                }

                return professor;
            }

            [HttpPost]
            public async Task<IActionResult> Post(Professor newProfessor)
            {
                await _professorService.CreateAsync(newProfessor);

                return CreatedAtAction(nameof(Get), new { id = newProfessor.Id }, newProfessor);
            }

            [HttpPut("{id:length(24)}")]
            public async Task<IActionResult> Update(string id, Professor updatedProfessor)
            {
                var professor = await _professorService.GetAsync(id);

                if (professor is null)
                {
                    return NotFound();
                }

                updatedProfessor.Id = professor.Id;

                await _professorService.UpdateAsync(id, updatedProfessor);

                return NoContent();
            }

            [HttpDelete("{id:length(24)}")]
            public async Task<IActionResult> Delete(string id)
            {
                var professor = await _professorService.GetAsync(id);

                if (professor is null)
                {
                    return NotFound();
                }

                await _professorService.RemoveAsync(id);

                return NoContent();
            }
            [HttpPost("{age}")]
            public async Task<IActionResult> Aggregation(int age)
            {
                var result = await _professorService.AggregateAsync(age);
                return Ok(result);
            }
        }

    }
