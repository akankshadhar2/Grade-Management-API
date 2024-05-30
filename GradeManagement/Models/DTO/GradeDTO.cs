namespace GradeManagement.Models.DTO
{
    public class GradeDTO
    {
        public Guid GradeId { get; set; }
        public string GradeName { get; set; }
        public string DisplayName { get; set; }
        public string CenterName { get; set; }  // This will hold the name of the center
    }
}
