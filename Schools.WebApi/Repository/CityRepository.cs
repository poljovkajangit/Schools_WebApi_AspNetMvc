using Microsoft.EntityFrameworkCore;
using SchoolWebApi.Data;
using SchoolWebApi.Dtos;
using SchoolWebApi.Mappers;
using SchoolWebApi.Model;
using SchoolWebApi.QueryObjects;
using SchoolWebApi.Repository.Interfaces;

namespace SchoolWebApi.Repository
{
    public class CityRepository : ICityRepository
    {

        private readonly SchoolsDataContext _context;
        public CityRepository(SchoolsDataContext schoolsDataContext)
        {
            _context = schoolsDataContext;
        }

        public bool Create(CityDto city)
        {
            var cityModel = city.ToCityFromCityDto();
            _context.Add(cityModel);
            return Save();
        }

        public bool Delete(int id)
        {
            var city = _context.Cities.Find(id);
            if (city != null)
            {
                _context.Remove(city);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool Exists(int id)
        {
            return _context.Cities.Any(c => c.Id == id);
        }

        public ICollection<City> GetAll()
        {
            return _context.Cities.ToList();
        }
        public async Task<ICollection<City>> GetAllAsync()
        {
            return await _context.Cities.ToListAsync();
        }
        public City GetById(int id)
        {
            return _context.Cities.Find(id);// .Where(c => c.Id == id).FirstOrDefault();
        }

        //from stored procedure, with input parameter
        public ICollection<City> GetByIsCapitol(bool isCapitol)
        {
            string isCapitolStr = isCapitol ? "1" : "0";

            var cities = _context.Cities.FromSql($"GetCapitolCities {isCapitolStr}");

            return cities.ToList();
        }

        public ICollection<City> GetByQuery(CityQuery query)
        {
            var cities = _context.Cities.AsQueryable();

            if (!string.IsNullOrEmpty(query.Name))
            {
                cities = cities.Where(c => c.Name.Contains(query.Name)).AsQueryable();
            }
            if (!string.IsNullOrEmpty(query.Code))
            {
                cities = cities.Where(c => c.Code.Contains(query.Code)).AsQueryable();
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals(nameof(City.Name), StringComparison.OrdinalIgnoreCase))
                {
                    cities = query.IsDescending ? cities.OrderByDescending(c => c.Name) : cities.OrderBy(c => c.Name).AsQueryable();
                }
                else if (query.SortBy.Equals(nameof(City.Code), StringComparison.OrdinalIgnoreCase))
                {
                    cities = query.IsDescending ? cities.OrderByDescending(c => c.Code) : cities.OrderBy(c => c.Code).AsQueryable();
                }
            }
            if (query.PageNumber != null && query.PageNumber > 0)
            {
                var skipPage = (query.PageNumber.Value - 1) * query.PageSize.Value;

                cities = cities.Skip(skipPage).Take(query.PageSize.Value);
            }

            return cities.ToList();
        }

        public City GetCityBySchoolId(int schoolId)
        {
            return _context.Schools.Where(s => s.Id == schoolId).Select(c => c.City).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(int id, CityDto cityDto)
        {
            var cityModel = _context.Cities.Find(id);

            if (cityModel != null)
            {
                cityModel.Address = cityDto.Address;
                cityModel.Code = cityDto.Code;
                cityModel.Name = cityDto.Name;
                _context.Update(cityModel);
                return Save();
            }
            else
            {
                return false;
            }
        }
    }
}
