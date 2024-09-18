using Microsoft.AspNetCore.Mvc;
using SchoolWebApi.Dtos;
using SchoolWebApi.Mappers;
using SchoolWebApi.QueryObjects;
using SchoolWebApi.Repository.Interfaces;

namespace SchoolWebApi.Controllers
{
    [ApiController]
    [Route("api/teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _Repository;

        public TeacherController(ITeacherRepository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeacherDto>))]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _Repository.GetAllAsync();

            var teachersDto = teachers.Select(t => t.ToTeacherDto());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(teachersDto);
        }

        [HttpGet]
        [Route("GetByQuery")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeacherDto>))]
        public IActionResult GetByQuery([FromQuery] TeacherQuery query)
        {
            var teachers = _Repository.GetByQuery(query);

            var teacherDtos = teachers.Select(t => t.ToTeacherDto());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(teacherDtos);
        }

        [HttpGet]
        [Route("GetById/{id:int}")]
        [ProducesResponseType(200, Type = typeof(TeacherDto))]
        public IActionResult GetById([FromRoute] int id)
        {
            if (!_Repository.Exists(id))
            {
                return NotFound();
            }

            var teacher = _Repository.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacherDto = teacher.ToTeacherDto();

            return Ok(teacherDto);
        }

        [HttpGet]
        [Route("GetTeachersBySchoolId/{schoolId:int}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeacherDto>))]
        public IActionResult GetTeachersBySchoolId([FromRoute] int schoolId)
        {
            var teachers = _Repository.GetTeachersBySchoolId(schoolId);

            var teachersDto = teachers.Select(t => t.ToTeacherDto());

            return Ok(teachersDto);
        }
    }
}
