using Microsoft.EntityFrameworkCore;
using ProyectoPooBuses.Database;
using ProyectoPooBuses.Services.Buses;
using ProyectoPooBuses.Services.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddTransient<IBusesServices, BusesServices>();
builder.Services.AddTransient<IRoutesServices, RoutesServices>();


builder.Services.AddDbContext<BusUseRegisterDbContext>(
    Options => Options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();