using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);


//Sql connection
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));



// Add services to the container.
builder.Services.AddTransient<BooksService>();
builder.Services.AddTransient<PublishersService>();
builder.Services.AddTransient<AuthorsService>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

AppDbInitializer.SeedRoles(app);

app.Run();
