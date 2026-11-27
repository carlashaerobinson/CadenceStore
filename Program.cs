using CadenceStore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CadenceStore.Models; // Lets Program.cs refer to EmailSettings.
using CadenceStore.Services; // Lets Program.cs refer to EmailSender.
using Microsoft.AspNetCore.Identity.UI.Services; // Lets Program.cs refer to IEmailSender.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();



// ASHLEY - MAKING A CONNECTION BETWEEN THE JSON EMAIL SETTINGS IN THE USER SECRETS AND THE EMAILSETTINGS MODEL
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
//ASHLEY - CONNECTING THE EMAIL SENDER INTERFACE TO THE EMAIL SENDER CLASS I CREATED TO HANDLE EMAIL CONFIRMATION
builder.Services.AddTransient<IEmailSender, EmailSender>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	await DbInitializer.InitializeAsync(scope.ServiceProvider);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

//Middleware to find pages, identify user, and determine what the user is allowed to do.
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
