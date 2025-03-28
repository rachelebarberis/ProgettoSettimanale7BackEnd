using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProgettoSettimanale7BackEnd.Data;
using ProgettoSettimanale7BackEnd.Models;
using ProgettoSettimanale7BackEnd.Services;
using ProgettoSettimanale7BackEnd.Settings;
using Serilog;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override("Syster", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Async(a => a.File("Log/Log_txt", rollingInterval: RollingInterval.Day))
    .WriteTo.Async(a => a.Console())
    .CreateLogger();

try
{
    Log.Information("Starting application.....");

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomersManager API", Version = "v1" });

        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Inserisci il token JWT nel formato: Bearer {token}",
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        };

        c.AddSecurityDefinition("Bearer", securityScheme);
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { securityScheme, new string[] {} }
        });
    });

    builder.Services.Configure<Jwt>(builder.Configuration.GetSection(nameof(Jwt)));

    builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        // Imposta se l'account deve essere confermato via email prima di poter accedere
        options.SignIn.RequireConfirmedAccount =
            builder.Configuration.GetSection("Identity").GetValue<bool>("RequireConfirmedAccount");

        // Imposta la lunghezza minima della password
        options.Password.RequiredLength =
            builder.Configuration.GetSection("Identity").GetValue<int>("RequiredLength");

        // Richiede che la password contenga almeno un numero
        options.Password.RequireDigit =
            builder.Configuration.GetSection("Identity").GetValue<bool>("RequireDigit");

        // Richiede almeno una lettera minuscola nella password
        options.Password.RequireLowercase =
            builder.Configuration.GetSection("Identity").GetValue<bool>("RequireLowercase");

        // Richiede almeno un carattere speciale nella password
        options.Password.RequireNonAlphanumeric =
            builder.Configuration.GetSection("Identity").GetValue<bool>("RequireNonAlphanumeric");

        // Richiede almeno una lettera maiuscola nella password
        options.Password.RequireUppercase =
            builder.Configuration.GetSection("Identity").GetValue<bool>("RequireUppercase");
    })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters()
           {
               // Indica se il sistema deve validare l'issuer (chi ha generato il token).
               ValidateIssuer = true,

               // Indica se il sistema deve validare l'audience (chi può usare il token).
               ValidateAudience = true,

               // Indica se il sistema deve verificare che il token non sia scaduto.
               ValidateLifetime = true,

               // Indica se il sistema deve validare la chiave di firma usata per generare il token.
               ValidateIssuerSigningKey = true,

               // Imposta l'issuer valido (deve corrispondere a quello usato per generare il token).
               ValidIssuer = builder.Configuration.GetSection(nameof(Jwt)).GetValue<string>("Issuer"),

               // Imposta l'audience valida (deve corrispondere a quella prevista per il token).
               ValidAudience = builder.Configuration.GetSection(nameof(Jwt)).GetValue<string>("Audience"),

               // Specifica la chiave segreta usata per firmare il token (deve corrispondere a quella usata per la generazione).
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection(nameof(Jwt)).GetValue<string>("SecurityKey")))
           };
       });


    builder.Services.AddScoped<UserManager<ApplicationUser>>();
    builder.Services.AddScoped<SignInManager<ApplicationUser>>();
    builder.Services.AddScoped<RoleManager<ApplicationRole>>();
    builder.Services.AddScoped<ArtistaService>();
    builder.Services.AddScoped<EventoService>();

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );


    builder.Host.UseSerilog();

    var app = builder.Build();

    app.UseCors(c =>
        c.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );

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

}
catch (Exception ex)
{
    Log.Error(ex.Message);
}
finally
{
    await Log.CloseAndFlushAsync();
}