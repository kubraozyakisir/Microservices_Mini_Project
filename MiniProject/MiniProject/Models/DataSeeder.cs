namespace MiniProject.Models
{
    public class DataSeeder
    {
        //DataSeeder, EF Core'da veritabanı oluşturma ve örnek verileri 
        // doldurma işlemini kolaylaştırmak için kullanılan bir yapıdır.
        //veritabanı oluşturma işlemini otomatikleştirir.Veritabanına örnek veriler ekleyerek yazılımın işleyişini test etmeye yardımcı olur.
       
        private readonly EmployeeDbContext employeeDbContext;
        public DataSeeder(EmployeeDbContext employeeDbContext)
        {
            this.employeeDbContext = employeeDbContext;
        }
        //DataSeeder sınıfı, EmployeeDbContext türündeki bir parametre alarak örnek verileri veritabanına eklemek için kullanılır.
        //Seed() metodunda, öncelikle employeeDbContext.Employees içinde herhangi bir veri var mı diye kontrol edilir.
        //Veri yoksa, örnek bir Employee listesi oluşturulur
        //ve AddRange() yöntemi ile veritabanına eklenir.
        //Son olarak, değişiklikler SaveChanges() yöntemi kullanılarak kaydedilir.
        public void Seed()
        {
            if (!employeeDbContext.Employee.Any())
            {
                var employees = new List<Employee>()
                {
                    new Employee()
                    {
                        EmployeeName = "Alex",
                        Citizenship="UK",
                        IdEmployee="1"
                    },
                    new Employee()
                    {
                        EmployeeName = "Juny",
                        Citizenship="AU",
                        IdEmployee="2"
                    }

                };
                employeeDbContext.Employee.AddRange(employees);
                employeeDbContext.SaveChanges();
            }
//bu kodlar, veritabanına örnek veriler eklemek için kullanılan bir DataSeeder sınıfı tanımlar.
//Seed() metodunda, örnek bir Employee listesi oluşturulur ve veritabanına eklenir. 
//Bu örnekler, Employee sınıfının özelliklerini taşır ve veritabanında kullanılabilecek verilerdir.
        }
    }
}
