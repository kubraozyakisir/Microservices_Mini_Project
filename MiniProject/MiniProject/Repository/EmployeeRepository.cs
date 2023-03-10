using MiniProject.Models;
using MiniProject.Repository.Interfaces;

namespace MiniProject.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext db;

        public EmployeeRepository(EmployeeDbContext db)
        {
            this.db = db;
        }
        public List<Employee> GetAll() => db.Employee.ToList();
        public Employee Update(Employee employee)
        {
            db.Employee.Update(employee);
            db.SaveChanges();
            return db.Employee.Where(x => x.IdEmployee == employee.IdEmployee).FirstOrDefault();
        }
        public List<Employee> Add(Employee employee)
        {
            db.Employee.Add(employee);
            db.SaveChanges();
            return db.Employee.ToList();
        }
        public Employee GetEmployee(string id)
        {
            return db.Employee.Where(x => x.IdEmployee == id).FirstOrDefault();
        }

    }
}
