using SchoolWebApi.Dtos;
using SchoolWebApi.Model;

namespace SchoolWebApi.Mappers
{
    public static class TeacherMapper
    {
        public static TeacherDto ToTeacherDto(this Teacher teacherModel)
        {

            return new TeacherDto
            {
                Id = teacherModel.Id,
                Name = teacherModel.Name
            };
        }

        public static City ToTeacherModelFromTeacherDto(this TeacherDto teacherDto)
        {
            return new City
            {
                Id = teacherDto.Id,
                Name = teacherDto.Name                
            };
        }

    }
}
