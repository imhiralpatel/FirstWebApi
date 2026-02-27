using Microsoft.AspNetCore.Mvc;
using FirstWebApi.Models;
using FirstWebApi.Services;
using FirstWebApi.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace FirstWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        // [HttpGet]
        // public async Task<IActionResult> GetAll()
        // {
        //     var employees = await _service.Employees
        //         .Include(e => e.Department)
        //         .ToListAsync();

        //     return Ok(employees);
        // }
        
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var emp = _service.GetById(id);
            if (emp == null) return NotFound();
            return Ok(emp);
        }

        // [HttpPost]
        // public IActionResult Post(Employee emp)
        // {
        //     return Ok(_service.Add(emp));
        // }
        
        [Authorize]
        [HttpPost]
        public IActionResult Create(EmployeeCreateDto dto)
        {
            var employee = new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                DepartmentId = dto.DepartmentId
            };

            var created = _service.Add(employee);

            var response = new EmployeeResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                Email = created.Email,
                DepartmentId = created.DepartmentId
            };

            return Ok(response);
        }

        
        [HttpPut("{id}")]
        public IActionResult Update(int id, Employee employee)
        {
            var updated = _service.Update(id, employee);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_service.Delete(id)) return NotFound();
            return Ok("Deleted Successfully");
        }

    }
}
