using Microsoft.EntityFrameworkCore;
using Store.Bussines.Interfaces;
using Store.Bussines;
using Store.Data.Data;
using Store.Data.Repositories.Interfaces;
using Store.Data.Repositories;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Store.Bussines.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// conexion a base de datos 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new ClienteProfile()); 
});

var jwtSecret = builder.Configuration["JwtSettings:Secret"];
var key = Encoding.ASCII.GetBytes(jwtSecret);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
// Add services to the container.

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//Repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IArticuloRepository, ArticuloRepository>(); 
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteArticuloRepository, ClienteArticuloRepository>();


//Bussiness
builder.Services.AddScoped<ITiendaService, TiendaBL>();
builder.Services.AddScoped<IArticuloService, ArticuloBL>();
builder.Services.AddScoped<IClienteService, ClienteBL>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICarritoService, CarritoService>();


builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()    // Permitir cualquier origen
                  .AllowAnyMethod()    // Permitir cualquier método (GET, POST, PUT, DELETE)
                  .AllowAnyHeader();   // Permitir cualquier header
        });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope()) 
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
