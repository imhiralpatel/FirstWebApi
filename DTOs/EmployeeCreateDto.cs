using System.ComponentModel.DataAnnotations;

namespace FirstWebApi.DTOs
{
    public class EmployeeCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }
        
        // [Required]
        // public string Department { get; set; } = string.Empty;

    }
}
