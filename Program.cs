using Microsoft.EntityFrameworkCore;
using pruebaApiC_.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//Agregamos el servicios para la base de datos
builder.Services.AddDbContext<ClienteContext>(db => {

    db.UseSqlServer(builder.Configuration.GetConnectionString("conexion"));

});

builder.Services.AddCors(options =>
{

    options.AddPolicy("AllowAnyOrigin",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();
app.UseCors("AllowAnyOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
