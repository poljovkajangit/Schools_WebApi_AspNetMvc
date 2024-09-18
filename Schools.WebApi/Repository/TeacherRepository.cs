using Microsoft.EntityFrameworkCore;
using SchoolWebApi.Data;
using SchoolWebApi.Dtos;
using SchoolWebApi.Mappers;
using SchoolWebApi.Model;
using SchoolWebApi.QueryObjects;
using SchoolWebApi.Repository.Interfaces;

namespace SchoolWebApi.Repository
{
    public class TeacherRepository : ITeacherRepository
    {

        private readonly SchoolsDataContext _context;
        public TeacherRepository(SchoolsDataContext schoolsDataContext)
        {
            _context = schoolsDataContext;
        }

        public bool Create(TeacherDto teacherDto)
        {
            var teacherModel = teacherDto.ToTeacherModelFromTeacherDto();
            _context.Add(teacherModel);
            return Save();
        }

        public bool Delete(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher != null)
            {
                _context.Remove(teacher);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool Exists(int id)
        {
            return _context.Teachers.Any(c => c.Id == id);
        }

        public ICollection<Teacher> GetAll()
        {
            return _context.Teachers.ToList();
        }
        public async Task<ICollection<Teacher>> GetAllAsync()
        {
            return await _context.Teachers.ToListAsync();
        }
        public Teacher GetById(int id)
        {
            return _context.Teachers.Find(id); // or .Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Teacher> GetByQuery(TeacherQuery query)
        {
            var teachers = _context.Teachers.AsQueryable();

            if (!string.IsNullOrEmpty(query.Name))
            {
                teachers = teachers.Where(t => t.Name.Contains(query.Name)).AsQueryable();
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals(nameof(Teacher.Name), StringComparison.OrdinalIgnoreCase))
                {
                    teachers = query.IsDescending ? teachers.OrderByDescending(t => t.Name)
                                                    : teachers.OrderBy(t => t.Name).AsQueryable();
                }
            }
            if (query.PageNumber != null && query.PageNumber > 0)
            {
                var skipPage = (query.PageNumber.Value - 1) * query.PageSize.Value;

                teachers = teachers.Skip(skipPage).Take(query.PageSize.Value);
            }

            return teachers.ToList();
        }

        public ICollection<Teacher> GetTeachersBySchoolId(int schoolId)
        {
            return _context.SchoolTeachers.Where(st => st.SchoolId == schoolId).Select(t => t.Teacher).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(int id, TeacherDto teacherDto)
        {
            var teacherModel = _context.Teachers.Find(id);

            if (teacherModel != null)
            {
                teacherModel.Name = teacherDto.Name;
                _context.Update(teacherModel);
                return Save();
            }
            else
            {
                return false;
            }
        }
    }
}
