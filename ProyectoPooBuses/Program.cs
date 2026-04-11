using Microsoft.EntityFrameworkCore;
using ProyectoPooBuses.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BusUseRegisterDbContext>(Options => Options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();




app.Run();

