using e_commerce_api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(opt =>
    {

    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

try
{
    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(StoreContext).Name);
    context.Database.Migrate();
    DbInitializer.Initialize(context);
    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(StoreContext).Name);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(StoreContext).Name);
}   
app.Run();
