using GymBroApi.Services;
using GymBroApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 👇 Replace AddSingleton<GymBroService> with these two
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=gymbros.db"));

builder.Services.AddScoped<GymBroService>();




var app = builder.Build();

app.UseCors("AllowReact");
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();