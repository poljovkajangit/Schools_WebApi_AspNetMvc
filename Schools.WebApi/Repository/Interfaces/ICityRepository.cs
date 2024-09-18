using SchoolWebApi.Dtos;
using SchoolWebApi.Model;
using SchoolWebApi.QueryObjects;

namespace SchoolWebApi.Repository.Interfaces
{
    public interface ICityRepository
    {
        ICollection<City> GetAll();
        ICollection<City> GetByQuery(CityQuery query);
        ICollection<City> GetByIsCapitol(bool isCapitol);
        City GetById(int id);
        bool Exists(int id);
        City GetCityBySchoolId(int schoolId);
        bool Create(CityDto city);
        bool Update(int id, CityDto city);
        bool Delete(int id);
        bool Save();
        Task<ICollection<City>> GetAllAsync();
    }
}
