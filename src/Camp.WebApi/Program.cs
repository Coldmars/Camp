using Camp.Common.AutoMapper;
using Camp.DataAccess.Data;
using Camp.WebApi.Extensions;
using Camp.WebApi.Filters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ExceptionFilter>();
    })
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder
                                                                .Configuration
                                                                .GetConnectionString("DefaultConnection")));
builder.Services.AddApplicationServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();