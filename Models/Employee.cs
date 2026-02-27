namespace FirstWebApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Email { get; set; } = string.Empty;
        //public required string Department { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

    }
}
