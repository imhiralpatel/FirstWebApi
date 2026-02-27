using FirstWebApi.Models;
using FirstWebApi.Data;

namespace FirstWebApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        // private static List<Employee> employees = new List<Employee>();

        // public List<Employee> GetAll()
        // {
        //     return employees;
        // }

        // public void Add(Employee emp)
        // {
        //     employees.Add(emp);
        // }

        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }
        public Employee? GetById(int id)
        {
            return _context.Employees.Find(id);
        }
        public Employee Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }
        public Employee? Update(int id, Employee employee)
        {
            var existing = _context.Employees.Find(id);
            if (existing == null) return null;

            existing.Name = employee.Name;
            existing.Email = employee.Email;
            existing.DepartmentId = employee.DepartmentId;

            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null) return false;

            _context.Employees.Remove(emp);
            _context.SaveChanges();
            return true;
        }

    }
}
