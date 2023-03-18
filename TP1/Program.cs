using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Datos;
using Dominio.Contratos;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Services.Interfaces;
using Services;
using Services.Herramientas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Inyeccion de dependencias
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddTransient(typeof(IRepositorioGenerico<>), typeof(RepositorioGenerico<>))
    .AddScoped(typeof(ITokenHandlerService), typeof(TokenHandlerService));
builder.Services.AddTransient<ILineService, LineService>();
builder.Services.AddTransient<IModelService, ModelService>();
builder.Services.AddTransient<IColorService, ColorService>();
builder.Services.AddTransient<IDefectService, DefectService>();
builder.Services.AddTransient<ITurnService, TurnService>();
builder.Services.AddTransient<IOrdenService, OrdenService>();
builder.Services.AddTransient<IIncidenciaService, IncidenciaService>();

//cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var connstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services
    .AddSqlServer<ControlCalidadContexto>(connstring)
    .AddIdentityCore<Usuario>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ControlCalidadContexto>();

builder.Services.AddDbContext<ControlCalidadContexto>(option =>
    option.UseSqlServer(connstring)
    );

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("this is my custom Secret key for authentication")),
            RequireExpirationTime = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();