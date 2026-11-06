using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;

        public PeopleController([FromKeyedServices("peopleService2")]IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;

        [HttpGet("{id}")]
        // Agregar una persona en el método post
        // public ActionResult<People> GetCertainPeople(int id) 
        public IActionResult GetCertainPeople(int id)
        {
            var person = Repository.People.FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return NotFound($"No se encontró una persona con el ID: {id}.");
            }

            return Ok(person);
        }
        //public People GetCertainPeople(int id) => Repository.People.First(p => p.Id == id);
        //public People GetCertainPeople(int id) => Repository.People.Find(p => p.Id == id);


        [HttpGet("search/{search}")]
        public IActionResult GetName(string search)
        {
            var people = Repository.People.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();
       
              if (people.Count == 0)
            {
                return NotFound($"No se encontraron personas que coincidan con '{search}");
            }

            return Ok(people);
        }
        //public People GetName(string search) => Repository.People.Find(p=> string.Equals(p.Name, search, StringComparison.OrdinalIgnoreCase));
        /* public List<People> GetName(string search) =>
         Repository.People.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();
        */

          [HttpPost]
          public IActionResult Add (People person)
        { 
            if (!_peopleService.Validate(person))
            {
                return BadRequest("LOL");
            }
            Repository.People.Add(person);

            return NoContent();
        }

    }

    public class Repository()
    {

        public static List<People> People = new List<People>
        {
            new People (){Id=1, Name="Juan", Birthdate = new DateTime(2000, 10, 10)},
            new People (){Id=2, Name="Jesús", Birthdate = new DateTime(1997, 09, 27)},
            new People (){Id=3, Name="Fátima", Birthdate = new DateTime(1978, 03, 12)}
        };
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }

    }



}
