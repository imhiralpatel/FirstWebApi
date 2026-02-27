using FirstWebApi.Models;

namespace FirstWebApi.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
        //void Add(Employee emp);
        Employee? GetById(int id);
        Employee Add(Employee employee);
        Employee? Update(int id, Employee employee);
        bool Delete(int id);
    }
}
