using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.WebAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CrudSampleController : ControllerBase
    {
        // GET: api/<CrudSampleController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            // Read: Alle Daten lesen
            return new string[] { "value1", "value2" };
        }

        // GET api/<CrudSampleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            // Read: Daten lesen
            return "value";
        }

        // POST api/<CrudSampleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            // Create: Daten schreiben
        }

        // PUT api/<CrudSampleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            // Update: Daten aktualisieren
        }

        // DELETE api/<CrudSampleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // Delete: Daten löschen
        }
    }
}
