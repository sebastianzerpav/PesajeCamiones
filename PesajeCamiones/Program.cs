using PesajeCamiones.Data;
using PesajeCamiones.Services.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//DbContext Injection
DbContextConfiguration.Configuration(builder.Services, builder.Configuration);

//Services Injection 
ServicesConfiguration.Configuration(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
