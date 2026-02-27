using Microsoft.EntityFrameworkCore;
using FirstWebApi.Data;
using FirstWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔴 THIS LINE IS MUST
// Controllers
builder.Services.AddControllers();

// DB Context                                                                                      
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        "Server=JAYNA-18;Database=FirstWebApiDB;Trusted_Connection=True;TrustServerCertificate=True"
    ));

// Repository DI register

// Service DI register
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


//JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
        )
    };
});


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ===== MIDDLEWARE =====
app.UseMiddleware<FirstWebApi.Middlewares.ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 👇 ADD THIS
app.MapControllers();

app.Run();

