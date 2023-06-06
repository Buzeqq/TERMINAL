using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TerminalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonDbContext _context;

        public PersonController(PersonDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_context.People);
        }
    }
}
