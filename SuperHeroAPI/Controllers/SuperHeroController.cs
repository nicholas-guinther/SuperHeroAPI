using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {    
            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero Not Found");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            dataContext.SuperHeroes.Add(hero);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }

       [HttpPut]
       public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbhero = await dataContext.SuperHeroes.FindAsync(request.Id);
            if (dbhero == null)
            {
                return BadRequest("Hero not Found");
            }

            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName;
            dbhero.LastName = request.LastName;
            dbhero.Location = request.Location;

            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbhero = await dataContext.SuperHeroes.FindAsync(id);
            if (dbhero == null)
            {
                return BadRequest("Hero Not Found");
            }

            dataContext.SuperHeroes.Remove(dbhero);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }

    }
}
