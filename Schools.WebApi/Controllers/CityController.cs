using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolWebApi.Dtos;
using SchoolWebApi.Helpers;
using SchoolWebApi.Mappers;
using SchoolWebApi.QueryObjects;
using SchoolWebApi.Repository.Interfaces;

namespace SchoolWebApi.Controllers
{
    [ApiController]
    [Route("api/city")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _CityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _CityRepository = cityRepository;
        }


        [HttpGet]
        [Route("hello")]
        public IActionResult Hello()
        {
            return Ok(new CityDto() { Name = "name1111", Address = "address333", Code = "code222", Id = 1, IsCapitol = true });
        }

        [HttpGet]
        [Route("say")]
        public IActionResult say([FromQuery] string word)
        {
            return Ok($"You said {word} ABC 7777");
        }

        [HttpGet]
        [Route("GetByIsCapitol/{isCapitol:bool}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CityDto>))]
        //[Log]
        public IActionResult GetByIsCapitol([FromRoute] bool isCapitol)
        {
            var cities = _CityRepository.GetByIsCapitol(isCapitol);

            var citiesDto = cities.Select(c => c.ToCityDto());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(citiesDto);
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CityDto>))]
        //[Authorize]
        //[Log]
        public async Task<IActionResult> GetAll()
        {
            var cities = await _CityRepository.GetAllAsync();

            var citiesDto = cities.Select(c => c.ToCityDto());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(citiesDto);
        }

        [HttpGet]
        [Route("GetByQuery")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CityDto>))]
        public IActionResult GetByQuery([FromQuery] CityQuery cityQuery)
        {
            var cities = _CityRepository.GetByQuery(cityQuery);

            var citiesDto = cities.Select(c => c.ToCityDto());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(citiesDto);
        }

        [HttpGet]
        [Route("GetById/{id:int}")]
        [ProducesResponseType(200, Type = typeof(CityDto))]
        public IActionResult GetById([FromRoute] int id)
        {
            if (!_CityRepository.Exists(id))
            {
                return NotFound();
            }

            var city = _CityRepository.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cityDto = city.ToCityDto();

            return Ok(cityDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CityDto cityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _CityRepository.Create(cityDto);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CityDto cityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_CityRepository.Exists(id))
            {
                return NotFound();
            }

            var result = _CityRepository.Update(id, cityDto);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_CityRepository.Exists(id))
            {
                return NotFound();
            };

            _CityRepository.Delete(id);

            return Ok();
        }
    }
}
