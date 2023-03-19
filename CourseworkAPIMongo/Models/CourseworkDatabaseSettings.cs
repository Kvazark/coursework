namespace CourseworkAPIMongo.Models
{
    public class CourseworkDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string ProfessorsCollectionName { get; set; } = null!;
        public string StudentsCollectionName { get; set; } = null!;
        public string CreaturesCollectionName { get; set; } = null!;
        public string DatabaseName { get; set; }= null!;
    }
}