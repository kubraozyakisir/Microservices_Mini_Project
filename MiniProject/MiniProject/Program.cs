using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiniProject.Models;
using MiniProject.Repository;
using MiniProject.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddTransient<DataSeeder>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddDbContext<EmployeeDbContext>(x=>x.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwaggerUI();

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

app.UseSwagger(x=>x.SerializeAsV2=true);
app.MapGet("/employee/{id}",([FromServices] IEmployeeRepository db,string id)=>
{
    return db.GetEmployee(id);
});

app.MapGet("/employees", ([FromServices] IEmployeeRepository db) =>
{
    return db.GetAll();
});
app.MapPut("/employee/{id}", ([FromServices] IEmployeeRepository db, Employee employee) =>
{
    return db.Update(employee);
});
app.MapPost("/employee", ([FromServices] IEmployeeRepository db, Employee employee) =>
{

    return db.Add(employee);
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
