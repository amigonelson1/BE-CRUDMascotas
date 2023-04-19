using BE_CRUDMascotas.Models;
using BE_CRUDMascotas.Models.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//lo siguiente es para correcion de politicas de cors();
builder.Services.AddCors(options => options.AddPolicy("AllowWebapp",
                                    builder => builder.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()));

//Aqui agregamos el contexto;
builder.Services.AddDbContext<AplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
});

//automapper
builder.Services.AddAutoMapper(typeof(Program));

//adicion de servicios;
builder.Services.AddScoped<IMascotaRepository, MascotaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//habilitamos la funcion de los cors();
app.UseCors("AllowWebapp"); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
