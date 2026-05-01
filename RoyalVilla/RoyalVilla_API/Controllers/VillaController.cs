using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RoyalVilla_API.Data;
using RoyalVilla_API.DTO;
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
        private readonly IMapper _mapper;
        public VillaController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Villa>> GetVillByID(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Villa ID must be greater than 0 ");
                }
                var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);

                if (villa == null)
                {
                    return NotFound($"Villa with ID {id} not found.");
                }
                return Ok(villa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occured while retrieving villa with ID {id}:{ex.Message}");
            }
        }
        [HttpPost()]
        public async Task<ActionResult<Villa>> CreateVilla(VillaCreateDTO  villaDTO)
        {
            try
            {
                if (villaDTO == null)
                {
                    return BadRequest("Invalid villa data.");
                }
                //Villa villa = new()
                //{
                //    Name = villaDTO.Name,
                //    Details = villaDTO.Details,
                //    Rate = villaDTO.Rate,
                //    Sqft = villaDTO.Sqft,
                //    Occupancy = villaDTO.Occupancy,
                //    ImageUrl = villaDTO.ImageUrl,
                //    CreatedDate = DateTime.Now
                //};

                var villa = _mapper.Map<Villa>(villaDTO);

                await _db.Villas.AddAsync(villa);
                await _db.SaveChangesAsync();
                //return Ok(villa);
                return CreatedAtAction(nameof(CreateVilla), new { id = villa.Id }, villa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occured while creating villa:{ex.Message}");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Villa>> UpdateVilla(int id, VillaUpdateDTO villaDTO)
        {
            try
            {
                if (villaDTO == null)
                {
                    return BadRequest("Invalid villa data.");
                }
                if (id <= 0)
                {
                    return BadRequest("Invalid villa ID.");
                }
                if(id != villaDTO.id)
                {
                    return BadRequest("Villa ID mismatch.");
                }


                var existingVilla = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);

                if (existingVilla == null)
                {
                    return NotFound($"Villa with ID {id} not found.");
                }
                _mapper.Map(villaDTO, existingVilla);
                existingVilla.UpdatedDate = DateTime.Now;

                await _db.SaveChangesAsync();

                return Ok(villaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occured while updating villa:{ex.Message}");
            }
        }
    }
}
