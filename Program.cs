using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using webApi_build_Real.ApplictionContext;
using webApi_build_Real.Helper;
using webApi_build_Real.Repository.implementation;

var builder = WebApplication.CreateBuilder(args);

//...Add services to the container.... 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DataContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repository-->
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//automapper

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);


//builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped<Startup>();
//var cin= new SqlConnectionStringBuilder(
//    builder.Configuration.GetConnectionString("DefaultConnection"));
//       builder.Password  = builder.Configuration.GetSection("DBPassword").Value;


var connectionString = builder.Configuration.GetConnectionString;
//using for token==>
//var secretKey = builder.Configuration.GetSection("AppSettings:Key").Value;
//var key = new SymmetricSecurityKey(Encoding.UTF8
//            .GetBytes(secretKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    opt.TokenValidationParameters= new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
       // IssuerSigningKey= key

    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}


//goboly exception handling
//app.ConfigureExceptionHandler(env);


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
