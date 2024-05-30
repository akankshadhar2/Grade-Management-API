using System.Diagnostics;

namespace GradeManagement.Models.Domain
{
    public class Center
    {
        
            public Guid CenterId { get; set; }
            public string Name { get; set; }

            // Navigation Property
            public IEnumerable<Grade> Grades { get; set; }
        
    }
}
