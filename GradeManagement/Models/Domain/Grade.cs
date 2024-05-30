namespace GradeManagement.Models.Domain
{
    public class Grade
    {
        public Guid GradeId { get; set; } 
        public string GradeName { get; set; }
        public string DisplayName { get; set; }

        // Navigation Property
        public Center Center { get; set; }

        // Foreign Key for Center
        public Guid CenterId { get; set; }
    }
}
