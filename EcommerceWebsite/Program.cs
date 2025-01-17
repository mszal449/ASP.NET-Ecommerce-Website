using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EcommerceWebsite.Data;
using EcommerceWebsite.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSession();
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
    await CreateAdminUser(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

async Task CreateAdminUser(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string adminEmail = "abc@gmail.com";
    string adminPassword = "abc";

    // Check if the Admin role exists, if not, create it
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Check if the admin user exists, if not, create it
    var user = await userManager.FindByEmailAsync(adminEmail);
    if (user == null)
    {
        user = new User
        {
            UserName = adminEmail,
            Email = adminEmail,
            IsAdmin = true
        };
        await userManager.CreateAsync(user, adminPassword);
        await userManager.AddToRoleAsync(user, "Admin");
    }
    else
    {
        // Ensure the user is in the Admin role and IsAdmin is set to true
        if (!await userManager.IsInRoleAsync(user, "Admin"))
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
        user.IsAdmin = true;
        await userManager.UpdateAsync(user);
    }
}
