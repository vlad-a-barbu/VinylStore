using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using VinylStore.Application.Services;
using VinylStore.DataAccess;
using VinylStore.DataAccess.EF;
using VinylStore.DataAccess.EF.Models;
using VinylStore.DataAccess.Repositories;
using VinylStore.DataObjects.AuthenticationModels;
using VinylStore.Domain;
using VinylStore.Domain.Services;
using VinylStore.Web.Authorization;
using VinylStore.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<VinylStoreContext>(options => 
    options.UseSqlServer(builder.Configuration["VinylStoreConnection"]));

builder.Services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserAuthenticationService>();
builder.Services.AddScoped<JwtUtils>();

builder.Services.Configure<JwtSecret>(builder.Configuration.GetSection("JwtSecret"));

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterType<ServiceDependencies>();
    builder.RegisterType<UnitOfWork>();
    builder.RegisterType<GenreDomainService>();
    builder.RegisterType<UserDomainService>();
    builder.RegisterType<AddressDomainService>();
    builder.RegisterType<DomainServices>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.UseJwtAuthorization();

app.MapControllers();

app.Run();
