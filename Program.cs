using FileManagerAPI.Repository;
using FileManagerAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    //----------------------------------------------CONFIGURACION JWT START---------------------------------------------//

    builder.Configuration.AddJsonFile("appsettings.json");
    var secretKey = builder.Configuration.GetSection("settings").GetSection("secretkey").ToString();
    var keyBytes = Encoding.UTF8.GetBytes(secretKey);

    builder.Services.AddAuthentication(config =>
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = true;
        config.SaveToken = true;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

    //----------------------------------------------CONFIGURACION JWT END---------------------------------------------//

    //--------------------------------------------CONFIGURACION CORS START--------------------------------------------//

    builder.Services.AddCors(options =>

    options.AddPolicy("AllowWebApp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
    //--------------------------------------------CONFIGURACION CORS END--------------------------------------------//


    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    //----------------------------------------------CONFIGURACION SWAGGER START---------------------------------------------//
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Login & Registration API", Version = "v1", Description = "API para manejo de HVI y RECAP con seguridad JWT" });

        //DEFINING SECURITY
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Insertar token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
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
    //----------------------------------------------CONFIGURACION SWAGGER END---------------------------------------------//

    ////RGISTRAR AUTOMAPPER
    //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    //Depency injection for repository use
    builder.Services.AddScoped<IFileRepository, FileRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ILoginRepository, LoginRepository>();

}



var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //---------------------------------NECESARIO PARA CORS----------------------------------------//
    app.UseCors("AllowWebApp");
    //---------------------------------NECESARIO PARA CORS----------------------------------------//

    app.UseHttpsRedirection();

    //---------------------------------NECESARIO PARA JWT----------------------------------------//
    app.UseAuthentication();
    //---------------------------------NECESARIO PARA JWT----------------------------------------//

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}


