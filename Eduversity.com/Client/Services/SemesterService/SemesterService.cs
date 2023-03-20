namespace Eduversity.com.Client.Services.SemesterService
{
    public class SemesterService: ISemesterService
    {
        public List<Semester> Semesters => new()
            {
                new Semester { Name = "First Semester", Value = "First" },
                new Semester { Name = "Second Semester", Value = "Second" },
                new Semester { Name = "Summer Semester", Value = "Summer" }
            };
    }
}
