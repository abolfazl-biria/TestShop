using EndPoint.Api.Extensions.DependencyInjection;
using EndPoint.Api.Extensions.Middleware;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var environment = builder.Environment;

// Add services to the container.

services
    .AddServices()
    .AddConfiguredDatabase(configuration)
    .AddConfiguredSwagger()
    .AddConfiguredMediatR()
    .AddConfiguredValidation();

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseConfiguredSwagger();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseConfiguredExceptionHandler(environment);

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

//    // نمونه داده اضافه کن (اختیاری)
//    if (!dbContext.Products.Any())
//    {SeedData
//        dbContext.Products.Add(new Product { Name = "Test Product", Price = 100 });
//        dbContext.SaveChanges();
//    }
//}

app.MapControllers();

app.Run();