using FestivalTicketsApp.WebUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddExternalAuthentication();

builder.Services.AddServices();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// app.MapRazorPages();

try
{
    await app.SeedData();
}
catch (Exception ex)
{
    // Logging will be implemented in future
}

app.Run();