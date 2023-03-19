using System.Collections.Generic;
using CourseworkAPIMongo.Models;
using CourseworkAPIMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkAPIMongo.Controllers
{
    public class ProfessorController
    {
        [Microsoft.AspNetCore.Components.Route("api/[controller]")]
        [ApiController]
        public class ProfessorsController : ControllerBase
        {
            private readonly ProfessorService _professorService;

            public ProfessorsController(ProfessorService professorService)
            {
                _professorService = professorService;
            }

            [HttpGet]
            public ActionResult<List<Professor>> Get() =>
                _professorService.Get();

            [HttpGet("{id:length(24)}", Name = "GetProfessor")]
            public ActionResult<Professor> Get(string id)
            {
                var professor = _professorService.Get(id);

                if (professor == null)
                {
                    return NotFound();
                }

                return professor;
            }

            [HttpPost]
            public ActionResult<Professor> Create(Professor professor)
            {
                _professorService.Create(professor);

                return CreatedAtRoute("GetProfessor", new { id = professor.Id.ToString() }, professor);
            }

            [HttpPut("{id:length(24)}")]
            public IActionResult Update(string id, Professor professorIn)
            {
                var professor = _professorService.Get(id);

                if (professor == null)
                {
                    return NotFound();
                }

                _professorService.Update(id, professorIn);

                return NoContent();
            }

            [HttpDelete("{id:length(24)}")]
            public IActionResult Delete(string id)
            {
                var professor = _professorService.Get(id);

                if (professor == null)
                {
                    return NotFound();
                }

                _professorService.Remove(id);

                return NoContent();
            }
        }
    }
}