using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using VinylStore.Application;
using VinylStore.DataAccess;
using VinylStore.DataAccess.EF;
using VinylStore.Domain;
using VinylStore.Domain.Services;

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

builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserAuthenticationService>();

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterType<ServiceDependencies>();
    builder.RegisterType<UnitOfWork>();
    builder.RegisterType<GenreDomainService>();
    builder.RegisterType<UserDomainService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
