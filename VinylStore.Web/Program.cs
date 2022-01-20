using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VinylStore.Application.Services;
using VinylStore.DataAccess;
using VinylStore.DataAccess.EF;
using VinylStore.DataAccess.Repositories;
using VinylStore.DataObjects.AuthenticationModels;
using VinylStore.Domain;
using VinylStore.Domain.Services;
using VinylStore.Web.Authorization;
using VinylStore.Web.Middleware;
using EFModels = VinylStore.DataAccess.EF.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "User",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<VinylStoreContext>(options => 
    options.UseSqlServer(builder.Configuration["VinylStoreConnection"]));

builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ArtistService>();
builder.Services.AddScoped<AlbumService>();
builder.Services.AddScoped<UserAuthenticationService>();
builder.Services.AddScoped<JwtUtils>();
builder.Services.AddTransient<IGenericRepository<EFModels.User>, GenericRepository<EFModels.User>>();

builder.Services.Configure<JwtSecret>(builder.Configuration.GetSection("JwtSecret"));

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterType<ServiceDependencies>();
    builder.RegisterType<UnitOfWork>();
    builder.RegisterType<GenreDomainService>();
    builder.RegisterType<UserDomainService>();
    builder.RegisterType<AddressDomainService>();
    builder.RegisterType<ArtistDomainService>();
    builder.RegisterType<AlbumDomainService>();
    builder.RegisterType<DomainServices>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseJwtAuthorization();

app.MapControllers();

app.Run();
