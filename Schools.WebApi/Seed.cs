using SchoolWebApi.Data;
using SchoolWebApi.Model;

namespace SchoolWebApi
{
    public class Seed
    {
        private readonly SchoolsDataContext _schoolsDataContext;

        public Seed(SchoolsDataContext schoolsDataContext)
        {
            this._schoolsDataContext = schoolsDataContext;
        }

        public void SeedDataContext()
        {
            if (!_schoolsDataContext.SchoolTeachers.Any())
            {
                var schoolTeachers = new List<SchoolTeacher>()
                {
                    new SchoolTeacher()
                    {

                          Teacher = new Teacher()
                          {
                              Name = "Suzy"
                          },
                          School = new School()
                          {
                              Name="School_1",
                              IsPrimary = true,
                              City = new City()
                              {
                                  Name = "city_1",
                                  Address = "city_address_1",
                                  Code = "city_1_code"
                              }
                          },
                          NumberOfSubjects = 3,
                          Salary = (decimal)99.99
                    }
                };

                var students = new List<Student>()
                {
                    new Student()
                    {
                        Name = "student_1",
                        Age = 12,
                        IsOnBudget = true,
                        School = schoolTeachers.First().School
                    }
                };

                _schoolsDataContext.AddRange(schoolTeachers);
                _schoolsDataContext.AddRange(students);
                _schoolsDataContext.SaveChanges();
            }
        }
    }
}
