using MiniProject.Models;

namespace MiniProject.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> Add(Employee employee);
        List<Employee> GetAll();
        Employee Update(Employee employee);
        public Employee GetEmployee(string id);
    }
}
