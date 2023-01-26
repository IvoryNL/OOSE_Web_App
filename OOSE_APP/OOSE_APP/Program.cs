using Logic.Models;
using Logic.Services;
using Logic.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IGebruikerService, GebruikerService>();
builder.Services.AddScoped<IKlasService, KlasService>();
builder.Services.AddScoped<IGebruikerService, GebruikerService>();
builder.Services.AddScoped<IOpleidingService, OpleidingService>();
builder.Services.AddScoped<IOpleidingsprofielService, OpleidingsprofielService>();
builder.Services.AddScoped<ILeeruitkomstService, LeeruitkomstService>();
builder.Services.AddScoped<ILesmateriaalService, LesmateriaalService>();
builder.Services.AddScoped<ILesService, LesService>();
builder.Services.AddScoped<ITentamenService, TentamenService>();
builder.Services.AddScoped<IOnderwijsuitvoeringService, OnderwijsuitvoeringService>();
builder.Services.AddScoped<IToetsinschrijvingService, ToetsinschrijvingService>();
builder.Services.AddScoped<IOnderwijsmoduleService, OnderwijsmoduleService>();
builder.Services.AddScoped<IOnderwijseenheidService, OnderwijseenheidService>();
builder.Services.AddScoped<ILeerdoelService, LeerdoelService>();
builder.Services.AddScoped<IConsistentieCheckService, ConsistentieCheckService>();
builder.Services.AddScoped<IBeoordelingsmodelService, BeoordelingsmodelService>();
builder.Services.AddScoped<IBeoordelingsonderdeelService, BeoordelingsonderdeelService>();
builder.Services.AddScoped<IBeoordelingsCriteriaService, BeoordelingscriteriaService>();
builder.Services.AddScoped<IBeoordelingsdimensieService, BeoordelingsdimensieService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=index}/{id?}");

app.Run();
