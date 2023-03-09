using System.ComponentModel.DataAnnotations;

namespace MiniProject.Models
{
    public class Employee
    {
        [Key]
        public string IdEmployee { get; set; }
        public string EmployeeName { get; set; }
        public string Citizenship { get; set; }
    }
    //public class EmployeeCollection
    //{
    //    public List<Employee> Employees { get; set; }
    //    public List<Employee> GetAllEmployee()
    //    {
    //        return new List<Employee>()
    //        {
    //            new Employee()
    //            {
    //                EmployeeName ="ALİ",
    //                IdEmployee = "520",
    //                Citizenship = "TC"
    //            },
    //            new Employee() {
    //                EmployeeName ="JACK",
    //                IdEmployee = "50",
    //                Citizenship = "USA"
    //            }

    //        };
    //    }
    //}
}
