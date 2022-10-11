using MarketPointApi;
using MarketPointApi.Utilidades;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorAzureStorage>();


//Conexion con bases de datos SQL server
var connectionString = "Data Source=LAPTOP-CTV90VT4\\SQLEXPRESS;Initial Catalog=MarketPointApii;Integrated Security=True; TrustServerCertificate=True";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString));


//Conexion FrontEnd (React)
var MyAllowSpecificOrigins = "frontend_url";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
        });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Conexion front
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
