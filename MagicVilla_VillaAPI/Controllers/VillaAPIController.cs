using HtmlAgilityPack;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    //[Route("api/[contoller]")]
    [Route("api/VillaAPI/[action]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        //custom logger
        private readonly ILogging _loger;
        private readonly ApplicationDbContext _db;
        public VillaAPIController(ILogger<VillaAPIController> logger, ILogging loger,ApplicationDbContext db)
        {
                _logger = logger;
            _loger = loger;
            _db = db;
        }
        private byte[] GetImageData(string ImageUrl)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    return webClient.DownloadData(ImageUrl);
                }
                catch (Exception)
                {
                    // Handle the exception if the image data cannot be retrieved
                    return null;
                }
            }
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            //_logger.LogInformation("Getting all villas");
            _loger.Log("Getting all villas", "");
            //return Ok(VillaStore.villaList);

            //return Ok(_db.Villas.ToList()); 
            var items = _db.Villas.ToList();
            var responseList = new List<VillaDTO>();
            foreach (var item in items)
            {
                // Get the main image URL using HtmlAgilityPack
                byte[] thumbnailUrl = GetImageData(item.ImageUrl);
                HttpResponseMessage httpresponse = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(thumbnailUrl)
                };
                httpresponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpg");
                string base64Image = Convert.ToBase64String(thumbnailUrl);
                // Create a response object with all properties
                VillaDTO response = new()
                {
                    Amenity = item.Amenity,
                    Details = item.Details,
                    Id = item.Id,
                    ImageUrl = item.ImageUrl,
                    Name = item.Name,
                    Occupancy = item.Occupancy,
                    Rate = item.Rate,
                    Sqft = item.Sqft,
                    ThumbnailUrl = base64Image
                };

                responseList.Add(response);
                _db.SaveChanges();
            }

            return Ok(responseList);
        }
    
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //Alternative ways
        //[ProducesResponseType(200, Type = typeof(VillaDTO))] here we remove VillaDTO from the method remaining only ActionResult
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                //_logger.LogError("Get Villa Error with Id" + id);
                _loger.Log("Get Villa Error with Id" + id,"error");

                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO) {
            //if(!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }
            //custom validation
            if (/*VillaStore.villaList*/_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null) {
                ModelState.AddModelError("CustomError", "villa already exists");
                return BadRequest(ModelState);
            }

            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            //VillaStore.villaList.Add(villaDTO);
            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                CreatedDate = DateTime.Now            };
            _db.Villas.Add(model);
            _db.SaveChanges();  
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa = /*VillaStore.villaList*/_db.Villas.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
           /* VillaStore.villaList*/_db.Villas.Remove(villa);
             _db.SaveChanges();  
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            if(villaDTO == null || id != villaDTO.Id) {
            return BadRequest();
            }
            //   var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            //villa.Name = villaDTO.Name;
            //   villa.Sqft = villaDTO.Sqft;
            //   villa.Occupancy = villaDTO.Occupancy;
            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                ThumbnailUrl = villaDTO.ThumbnailUrl,
               UpdatedDate = DateTime.Now
            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();
        }



        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePartialValue(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa = /*VillaStore.villaList*/_db.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);
            
            VillaDTO villaDTO = new()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
                ThumbnailUrl = villa.ThumbnailUrl,
                UpdatedDate = villa.UpdatedDate
            };
            if (villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villaDTO, ModelState);
            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                ThumbnailUrl = villaDTO.ThumbnailUrl,
                UpdatedDate = DateTime.Now

            };
            _db.Villas.Update(model);
            _db.SaveChanges();  
            if (!ModelState.IsValid)
            {
             return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}

