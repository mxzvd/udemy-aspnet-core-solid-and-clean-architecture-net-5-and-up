using System.Reflection;
using HR.LeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = new PathString("/users/login");
});

// builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri("https://localhost:7273"));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
// builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
// builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();

// builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCookiePolicy();
app.UseAuthentication();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// app.UseMiddleware<RequestMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
