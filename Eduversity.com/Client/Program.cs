global using Blazored.LocalStorage;
global using Blazored.Toast;
global using Eduversity.com.Client;
global using Eduversity.com.Client.Services.AdminService;
global using Eduversity.com.Client.Services.AuthService;
global using Eduversity.com.Client.Services.CountryService;
global using Eduversity.com.Client.Services.CourseService;
global using Eduversity.com.Client.Services.CourseStructureService;
global using Eduversity.com.Client.Services.DepartmentHeadService;
global using Eduversity.com.Client.Services.DepartmentOptionService;
global using Eduversity.com.Client.Services.DepartmentService;
global using Eduversity.com.Client.Services.FacultyService;
global using Eduversity.com.Client.Services.GenderService;
global using Eduversity.com.Client.Services.LecturerService;
global using Eduversity.com.Client.Services.LevelService;
global using Eduversity.com.Client.Services.LGAService;
global using Eduversity.com.Client.Services.MaritalStatusService;
global using Eduversity.com.Client.Services.PassportService;
global using Eduversity.com.Client.Services.SemesterService;
global using Eduversity.com.Client.Services.SessionService;
global using Eduversity.com.Client.Services.StateService;
global using Eduversity.com.Client.Services.StudentService;
global using Eduversity.com.Client.Services.ToggleMenuService;
global using Eduversity.com.Client.Services.ToggleService;
global using Eduversity.com.Client.Services.UserAddressService;
global using Eduversity.com.Shared.Dtos.CountryDto;
global using Eduversity.com.Shared.Dtos.CourseDto;
global using Eduversity.com.Shared.Dtos.CourseStructureDto;
global using Eduversity.com.Shared.Dtos.DepartmentDto;
global using Eduversity.com.Shared.Dtos.DepartmentOptionDto;
global using Eduversity.com.Shared.Dtos.FacultyDto;
global using Eduversity.com.Shared.Dtos.LecturerDto;
global using Eduversity.com.Shared.Dtos.LGADto;
global using Eduversity.com.Shared.Dtos.StateDto;
global using Eduversity.com.Shared.Dtos.StudentDto;
global using Eduversity.com.Shared.Dtos.UserAccountDto;
global using Eduversity.com.Shared.Dtos.UserAddressDto;
global using Eduversity.com.Shared.Models;
global using Eduversity.com.Shared.Models.Data;
global using Eduversity.com.Shared.Services;
global using Microsoft.AspNetCore.Components.Authorization;
global using Microsoft.AspNetCore.Components.Web;
global using System.Net.Http.Json;
using Eduversity.com.Client.Providers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IUserAddressService, UserAddressService>();

builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<ILGAService, LGAService>();

builder.Services.AddScoped<IFacultyService, FacultyService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentOptionService, DepartmentOptionService>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ILecturerService, LecturerService>();
builder.Services.AddScoped<IPassportService, PassportService>();
builder.Services.AddScoped<IDepartmentHeadService, DepartmentHeadService>();

builder.Services.AddScoped<IToggleService, ToggleService>();
builder.Services.AddScoped<IToggleMenuService, ToggleMenuService>();

builder.Services.AddScoped<IGenderService, GenderService>();
builder.Services.AddScoped<IMaritalStatusService, MaritalStatusService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<ILevelService, LevelService>();
builder.Services.AddScoped<ISemesterService, SemesterService>();

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseStructureService, CourseStructureService>();


builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();


builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddBlazoredToast();
builder.Services.AddSyncfusionBlazor();
// Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTQyNzA2N0AzMjMwMmUzNDJlMzBRbmVQMGhET1pzUVUzcEtiZXNFMk8ySU1FU0kzYk1XMEJOdHQxNmNJdTV3PQ==");

await builder.Build().RunAsync();
