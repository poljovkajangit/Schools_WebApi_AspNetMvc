using SchoolWebApi.Dtos;
using SchoolWebApi.Model;

namespace SchoolWebApi.Mappers
{
    public static class CityMappers
    {
        public static CityDto ToCityDto(this City cityModel)
        {

            return new CityDto
            {
                Address = cityModel.Address,
                Code = cityModel.Code,
                Id = cityModel.Id,
                Name = cityModel.Name,
                IsCapitol = cityModel.IsCapitol
            };
        }

        public static City ToCityFromCityDto(this CityDto cityDto)
        {
            return new City
            {
                Address = cityDto.Address,
                Code = cityDto.Code,
                Id = cityDto.Id,
                Name = cityDto.Name,
                IsCapitol = cityDto.IsCapitol
            };
        }

    }
}
