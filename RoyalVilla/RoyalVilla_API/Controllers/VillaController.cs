using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalVilla_API.Data;
using RoyalVilla_API.Model;

namespace RoyalVilla_API.Controllers
{
    [Route("api/villa")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        //[HttpGet]
        //public string GetVilla()
        //{
        //    return "Get all Villa";
        //}
        //[HttpGet("{id:int}")]
        //public string GetVillByID(int id)
        //{
        //    return "Villa is: "+id;
        //}
        //[HttpGet("{id:int}/{name}")]
        //public string GetVillByIDAndName([FromRoute] int id, [FromRoute] string name)
        //{
        //    return "Villa is: " + id + " :" + name;
        //}
        //[HttpGet()]
        //public string GetVillByIDAndName([FromQuery] int id, [FromQuery] string name)
        //{
        //    return "Villa is: " + id + " :" + name;
        //}
        //[HttpGet()]
        //public string GetVillByIDAndName([FromHeader] int id, [FromHeader] string name)
        //{
        //    return "Villa is: " + id + " :" + name;
        //}

        private readonly ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }

        //[HttpGet]
        //public IEnumerable<Villa> GetVilla()
        //{
        //    return _db.Villas.ToList();
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Villa>>> GetVilla()
        {
            return Ok(await _db.Villas.ToListAsync());
        }
    }
}
