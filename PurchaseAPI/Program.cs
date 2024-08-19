using DataAccessLayer.Contexts;
using DataAccessLayer.IRepositories;
using DataAccessLayer.Repositories;

using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddDbContext<PurchaseContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
builder.Services.AddScoped<IPurchaseRepository,PurchaseRepository>();

var keyCloakConfigSection = configuration.GetSection("Keycloak");
var authServerUrl = keyCloakConfigSection["Url"];
var realm = keyCloakConfigSection["Realm"];
var resource = keyCloakConfigSection["ClientId"];

if(authServerUrl is null || realm is null || resource is null)
{
    throw new InvalidOperationException("KeyCloak configuration is incomplete..!!");
}

var authenticationOptions = new KeycloakAuthenticationOptions
{
    AuthServerUrl = authServerUrl,
    Realm = realm,
    Resource = resource,
    VerifyTokenAudience = false,
    SslRequired = "false"
};

KeycloakProtectionClientOptions protectionOptions = new KeycloakProtectionClientOptions
{
    AuthServerUrl = authServerUrl,
    Realm = realm,
    Resource = resource
};

builder.Services.AddKeycloakAuthentication(authenticationOptions);
builder.Services.AddKeycloakAuthorization(protectionOptions);

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
