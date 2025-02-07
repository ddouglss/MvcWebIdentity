using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcWebIdentityA.Context;
<<<<<<< HEAD
using MvcWebIdentityA.Services;
=======
<<<<<<< HEAD
using MvcWebIdentityA.Services;
=======
>>>>>>> 23dde132088ba3ad476ff30ee0c52779fe2cbcfb
>>>>>>> 6c115c5858492edbe398a904299ef310bb3f663b

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnections");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 10;
    options.Password.RequiredUniqueChars = 3;
    options.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "AspNetCore.Cookies";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        options.SlidingExpiration = true;
    });

<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 6c115c5858492edbe398a904299ef310bb3f663b
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireUserAdminGerenteRole",
        policy => policy.RequireRole("User", "Admin", "Gerente"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdminClaimAccess",
        policy => policy.RequireClaim("CadastradoEm"));

    options.AddPolicy("IsAdminClaimAccess",
        policy => policy.RequireClaim("IsAdmin", "true"));

    options.AddPolicy("IsFuncionarioClaimAccess",
        policy => policy.RequireClaim("IsFuncionario", "true"));
});


builder.Services.AddScoped<ISeedUserRoleInitial, SeedUsersRoleInitial>();

builder.Services.AddScoped<ISeedUserClaimsInitial, SeedUsersClaimsInitial>();

<<<<<<< HEAD
=======

=======
>>>>>>> 23dde132088ba3ad476ff30ee0c52779fe2cbcfb
>>>>>>> 6c115c5858492edbe398a904299ef310bb3f663b
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
>>>>>>> 23dde132088ba3ad476ff30ee0c52779fe2cbcfb
>>>>>>> 6c115c5858492edbe398a904299ef310bb3f663b
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

<<<<<<< HEAD
await CriarPerfisUsuariosAsync(app);

=======
<<<<<<< HEAD
await CriarPerfisUsuariosAsync(app);

=======
>>>>>>> 23dde132088ba3ad476ff30ee0c52779fe2cbcfb
>>>>>>> 6c115c5858492edbe398a904299ef310bb3f663b
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 6c115c5858492edbe398a904299ef310bb3f663b
    name: "MinhaArea",
   pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");


app.MapControllerRoute(
<<<<<<< HEAD
=======
=======
>>>>>>> 23dde132088ba3ad476ff30ee0c52779fe2cbcfb
>>>>>>> 6c115c5858492edbe398a904299ef310bb3f663b
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 6c115c5858492edbe398a904299ef310bb3f663b

async Task CriarPerfisUsuariosAsync(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        //var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        //await service.SeedRolesAsync();
        //await service.SeedUsersAsync();

        var service = scope.ServiceProvider.GetService<ISeedUserClaimsInitial>();
        await service.SeedUserClaimsAsync();
    }
<<<<<<< HEAD
}
=======
}
=======
>>>>>>> 23dde132088ba3ad476ff30ee0c52779fe2cbcfb
>>>>>>> 6c115c5858492edbe398a904299ef310bb3f663b
