using Microsoft.EntityFrameworkCore;
using lab_1_Nyesteban.Models;
using lab_1_Nyesteban.DAL;
using System.Text.Json;
using lab_1_Nyesteban.Repositories.Interfaces;
using lab_1_Nyesteban.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*builder.Services.AddSqlite<StoreContext>(builder.Configuration.GetConnectionString("StoreContext") ?? "Data Source=StoreContext.db"/*,
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    }
    );*/
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<StoreContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("SampleDbConnection")));

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.IncludeFields = true;
});

builder.Services.AddScoped<IAppsRepository, AppsRepository>();
builder.Services.AddScoped<IGamesRepository, GamesRepository>();
builder.Services.AddScoped<ICompaniesRepository, CompaniesRepository>();
builder.Services.AddScoped<IDevelopmentDetailsRepository, DevelopmentDetailsRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => true).AllowCredentials());


/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}*/


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
