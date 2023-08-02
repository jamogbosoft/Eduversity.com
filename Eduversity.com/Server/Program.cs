global using AutoMapper;
global using Eduversity.com.Server.Data;
global using Eduversity.com.Server.Services.AuthService;
global using Eduversity.com.Server.Services.CountryService;
global using Eduversity.com.Server.Services.CourseAllocationService;
global using Eduversity.com.Server.Services.CourseService;
global using Eduversity.com.Server.Services.CourseStructureService;
global using Eduversity.com.Server.Services.DepartmentHeadService;
global using Eduversity.com.Server.Services.DepartmentOptionService;
global using Eduversity.com.Server.Services.DepartmentService;
global using Eduversity.com.Server.Services.EmailService;
global using Eduversity.com.Server.Services.FacultyService;
global using Eduversity.com.Server.Services.LecturerService;
global using Eduversity.com.Server.Services.LGAService;
global using Eduversity.com.Server.Services.StateService;
global using Eduversity.com.Server.Services.StudentService;
global using Eduversity.com.Server.Services.UserAddressService;
global using Eduversity.com.Shared.Dtos.CountryDto;
global using Eduversity.com.Shared.Dtos.CourseAllocationDto;
global using Eduversity.com.Shared.Dtos.CourseDto;
global using Eduversity.com.Shared.Dtos.CourseStructureDto;
global using Eduversity.com.Shared.Dtos.DepartmentDto;
global using Eduversity.com.Shared.Dtos.DepartmentOptionDto;
global using Eduversity.com.Shared.Dtos.EmailDto;
global using Eduversity.com.Shared.Dtos.FacultyDto;
global using Eduversity.com.Shared.Dtos.LecturerDto;
global using Eduversity.com.Shared.Dtos.LGADto;
global using Eduversity.com.Shared.Dtos.StateDto;
global using Eduversity.com.Shared.Dtos.StudentDto;
global using Eduversity.com.Shared.Dtos.UserAccountDto;
global using Eduversity.com.Shared.Dtos.UserAddressDto;
global using Eduversity.com.Shared.Models;
global using Eduversity.com.Shared.Services;
global using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)
        );
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IUserAddressService, UserAddressService>();

builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<ILGAService, LGAService>();

builder.Services.AddScoped<IFacultyService, FacultyService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentOptionService, DepartmentOptionService>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ILecturerService, LecturerService>();
builder.Services.AddScoped<IDepartmentHeadService, DepartmentHeadService>();

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseStructureService, CourseStructureService>();
builder.Services.AddScoped<ICourseAllocationService, CourseAllocationService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddHttpContextAccessor();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    app.UseSwaggerUI();
    app.UseSwagger();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
