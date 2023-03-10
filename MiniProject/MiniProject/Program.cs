using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiniProject.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddTransient<DataSeeder>();
builder.Services.AddDbContext<EmployeeDbContext>(x=>x.UseSqlServer(connectionString));
var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

//Seed Data
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataSeeder>();
        service.Seed();
    }
}
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
//app.MapGet("/", (Func<string>)(() => "Hello World!"));

//app.MapGet("/employee", (Func<Employee>)(() => {

//    return new Employee()
//    {
//        IdEmployee = "500",
//        Citizenship = "TC",
//        EmployeeName = "CAN",
//    };
//    }));
//app.MapGet("/employees", (Func<List<Employee>>)(() =>
//{

//    return new EmployeeCollection().GetAllEmployee();

//}));
//altta fromservices ayarlamalarý için miniprojectte langversion ekleriz.
app.MapGet("/employee/{id}",([FromServices] EmployeeDbContext db,string id)=>
{
    return db.Employee.Where(x => x.IdEmployee == id).FirstOrDefault();
});

app.MapGet("/employees", ([FromServices] EmployeeDbContext db) =>
{
    return db.Employee.ToList();
});
app.MapPut("/employee/{id}", ([FromServices] EmployeeDbContext db, Employee employee) =>
{
    db.Employee.Update(employee);
    db.SaveChanges();
    return db.Employee.Where(x => x.IdEmployee == employee.IdEmployee).FirstOrDefault();
});
app.MapPost("/employee", ([FromServices] EmployeeDbContext db, Employee employee) =>
{
    db.Employee.Add(employee);
    db.SaveChanges();
    return db.Employee.ToList();
});
//app.MapGet("/employee/{id}", async(http)=>
//{
//    if(!http.Request.RouteValues.TryGetValue("id",out var id)) 
//    {
//        http.Response.StatusCode = 400;
//        return;
//    }
//    else
//    {
//        await http.Response.WriteAsJsonAsync(new EmployeeCollection()
//            .GetAllEmployee().FirstOrDefault(x=>x.IdEmployee == (string)id));
//    }
//});
app.Run();
