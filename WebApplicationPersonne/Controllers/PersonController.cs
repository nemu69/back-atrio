using MaApplication;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationPerson.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly MyContext _context;

        public PersonsController(MyContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AjouterPerson([FromBody] Person Person)
        {
            if (Person.BirthDate > DateTime.Now.AddYears(-150))
            {
                _context.Persons.Add(Person);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("La Person doit avoir moins de 150 ans.");
            }
        }

        [HttpGet]
        public IEnumerable<object> GetPersons()
        {
            return _context.Persons
                .OrderBy(p => p.Name)
                .Select(p => new { p.Name, p.Surname, Age = DateTime.Now.Year - p.BirthDate.Year });
        }
    }
}