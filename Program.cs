using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentsUnite_II.Areas.Identity.Data;
using StudentsUnite_II.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("StudentsUnite_IIContextConnection") ?? throw new InvalidOperationException("Connection string 'StudentsUnite_IIContextConnection' not found.");

builder.Services.AddDbContext<StudentsUnite_IIContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ForumUser>(
    options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<StudentsUnite_IIContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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

app.UseAuthorization();

app.MapRazorPages();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using(var scope = app.Services.CreateScope())
{
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var roles = new[] { "Admin", "Member" };

foreach(var role in roles)
{
    if(!await roleManager.RoleExistsAsync(role))
    {
        await roleManager.CreateAsync(new IdentityRole(role));
    }
}

}


using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ForumUser>>();

    string email = "admin@admin.com";
    string password = "1234Asdf.";

    if(await userManager.FindByEmailAsync(email) == null)
    {
        var user = new ForumUser();
        user.Email = email;
        user.UserName = email;

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");

    }
}


app.Run();
