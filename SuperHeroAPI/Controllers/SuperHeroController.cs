using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Modeles;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext Context)
        {
            _context = Context;
        }

        /*public static List<SuperHero> heros = new List<SuperHero>
            {
                new SuperHero { Id = 1, Name = "Batman",FirstName ="Bruce",LastName="Wayne",Place ="Manhaten" },
                new SuperHero { Id = 2, Name = "SpiderMan",FirstName ="Piter",LastName="Parker",Place ="New York" },
                new SuperHero { Id = 3, Name = "StrawHate", FirstName = "Monkey", LastName = "D Luffy ", Place = "New world" }
            };*/

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeros.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetbyID([FromRouteAttribute] int Id)
        {
            var hero = await _context.SuperHeros.FindAsync(Id);
            if(hero == null)
            {
                return BadRequest("No hero found with this ID");
            }
            return Ok(hero);
        }

        /**
        [HttpPost]
        public async void AddHero(SuperHero newHero)
        {
            heros.Add(newHero);
        }*/

        [HttpPost]
        public async Task<ActionResult<string>> AddHero(SuperHero newHero)
        {
            _context.SuperHeros.Add(newHero);
            await _context.SaveChangesAsync();
            return Ok("Hero added");
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateHero(SuperHero newHero)
        {
            if (newHero == null)
                return BadRequest("");

            var dbhero = await _context.SuperHeros.FindAsync(newHero.Id);
            if (dbhero == null)
                return BadRequest("No hero found with this ID");

            dbhero.Name = newHero.Name;
            dbhero.FirstName = newHero.FirstName;
            dbhero.LastName = newHero.LastName;
            dbhero.Place = newHero.Place;
            await _context.SaveChangesAsync();
            return Ok("Update succeeded");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHero(int id)
        {
            var dbhero = await _context.SuperHeros.FindAsync(id);
            if (dbhero == null)
                return BadRequest("No hero found with this ID");

            _context.SuperHeros.Remove(dbhero);
            await _context.SaveChangesAsync();
            return Ok("Remove succeeded");

        }
    }
}
