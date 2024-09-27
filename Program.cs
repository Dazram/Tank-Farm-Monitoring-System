using Microsoft.EntityFrameworkCore;
using TFMS.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TFMSDBContext>(options =>
 options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(
    options => options.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .AllowAnyHeader()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
