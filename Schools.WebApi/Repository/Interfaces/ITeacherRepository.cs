using SchoolWebApi.Dtos;
using SchoolWebApi.Model;
using SchoolWebApi.QueryObjects;

namespace SchoolWebApi.Repository.Interfaces
{
    public interface ITeacherRepository
    {
        ICollection<Teacher> GetAll();
        ICollection<Teacher> GetByQuery(TeacherQuery query);
        Teacher GetById(int id);
        bool Exists(int id);
        ICollection<Teacher> GetTeachersBySchoolId(int schoolId);
        bool Create(TeacherDto city);
        bool Update(int id, TeacherDto teacher);
        bool Delete(int id);
        bool Save();
        Task<ICollection<Teacher>> GetAllAsync();
    }
}
