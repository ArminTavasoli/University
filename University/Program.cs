using Microsoft.EntityFrameworkCore;
using University.DbContexts;
using University.Repository;

string AllowedOrigin = "allowedOrigin";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Context
builder.Services.AddDbContext<UniversityDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration["ConnectionString:UniversityConnectionString"]);
});

//Add Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Repository
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();


// CORS
builder.Services.AddCors(option =>
{
    option.AddPolicy(AllowedOrigin, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<UniversityDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(AllowedOrigin);

app.MapControllers();

app.Run();
