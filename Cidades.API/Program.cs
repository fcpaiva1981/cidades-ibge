using AutoMapper;
using Cidades.Domain.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Cidades.Infrastructure.Mapper;
using Cidades.Infrastructure.Repository;
using Cidades.Infrastructure.Repository.Impementation;
using Cidadess.Application.Service;
using Cidadess.Application.Service.Implementation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CidadeesAPI", Version = "v1" });
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var connectionString = builder.Configuration.GetConnectionString("ConnectionStrings:SQLiteConnection") ?? "Data Source=sqliteCidades.db";
builder.Services.AddDbContext<SQLiteContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();
builder.Services.AddScoped<ICidadesService, CidadesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CidadeesAPI v1"));
 
}

await AsseguraDBExiste(app.Services);

app.UseAuthorization();

app.MapControllers();

app.Run();

async Task AsseguraDBExiste(IServiceProvider services)
{
    using var db = services.CreateScope().ServiceProvider.GetRequiredService<SQLiteContext>();
    await db.Database.EnsureCreatedAsync();
    await db.Database.MigrateAsync();
}