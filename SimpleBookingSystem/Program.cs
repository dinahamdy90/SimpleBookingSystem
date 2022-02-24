using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Infrastructure;
using SimpleBookingSystem.Infrastructure.Contracts;
using SimpleBookingSystem.Infrastructure.Services;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SimpleBookingSystem.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
       .AddFluentValidation(fvc =>
       {
           fvc.RegisterValidatorsFromAssemblyContaining<Program>();
           fvc.ConfigureClientsideValidation(enabled: false);
       });

builder.Services.AddTransient<ProblemDetailsFactory, GSProblemDetailsFactory>();


builder.Services.AddDbContext<SimpleBookingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SimpleBookingDB"), sqlOptions =>
    {
        sqlOptions.CommandTimeout(180);
        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, TimeSpan.FromSeconds(30), null);
    });

    options.EnableSensitiveDataLogging();
});

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly(), typeof(Program).Assembly);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .SetIsOriginAllowed((host) => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});
builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IResourceService, ResourceService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler("/error");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
 public partial class Program { }