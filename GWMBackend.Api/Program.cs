using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using GWMBackend.Api.Hubs;
using GWMBackend.Core.Model.Base;
using GWMBackend.Data.Base;
using GWMBackend.Domain.Models;
using GWMBackend.Service.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic.FileIO;
using Nancy;
using NLog.Targets.Wrappers;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("GWMBackend", new OpenApiInfo { Title = "GWMBackend", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
});
//var xz = SecurityHelpers.DecryptString(builder.Configuration["ConnectionString:DB"], "@Rp@2022!");
var xz = builder.Configuration["ConnectionString:DB"];
builder.Services.AddDbContext<GWM_DBContext>(options => options.UseSqlServer(xz), ServiceLifetime.Transient);
ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;


builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);




var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.TokenSecret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IServiceWrapper, ServiceWrapper>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
var domains = builder.Configuration["AppSettings:Domain"].Split(",");
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("reactApp", policyBuilder =>
    {
        policyBuilder.WithOrigins(domains)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
    opt.AddDefaultPolicy(builder => builder.SetIsOriginAllowed(option => new Uri(option).Host == "localhost")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) => httpReq.Scheme = httpReq.Host.Value);
});

app.UseRouting();

app.UseCors("reactApp");

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/GWMBackend/swagger.json", "GWMBackend v1"));
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/GWMBackend/swagger.json", "GWMBackend v1"));
//}
//else
//{
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/api/swagger/GWMBackend/swagger.json", "GWMBackend v1"));
//}


app.UseHttpsRedirection();

app.MapControllers();


app.MapHub<ChatHub>("/Chat");

app.Run();

