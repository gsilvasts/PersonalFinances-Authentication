using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using PersonalFinances.Authentication.Api.Extensions;
using PersonalFinances.Authentication.Api.Filters;
using PersonalFinances.Authentication.Application;
using PersonalFinances.Authentication.Application.Interfaces.Services;
using PersonalFinances.Authentication.Application.Models.InputModels;
using PersonalFinances.Authentication.Application.Validators;
using PersonalFinances.Authentication.Domain.Exceptions;
using PersonalFinances.Authentication.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "apiagenda", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = @"JWT Authorization header using the Bearer scheme.",
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

builder.Services
    .AddMongoDb()
    .AddRepositories()
    .AddApplicationServices()
    .AddAuthnAuthz()
    .AddValidatorsFromAssemblyContaining<SignUpValidator>()
    .AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/signup", async (IUserService service, SignUpInputModel inputModel, CancellationToken cancellationToken) =>
{
    try
    {
        var result = await service.SignUpAsync(inputModel, cancellationToken);

        return Results.Ok(result);
    }
    catch (DomainException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}).AddEndpointFilter<ValidationFilter<SignUpInputModel>>();

app.MapPost("/login", async (IUserService service, string email, string password, CancellationToken cancellationToken) =>
{
    try
    {
        var result = await service.SignInAsync(email, password, cancellationToken);

        return Results.Ok(result);

    }
    catch(DomainException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (Exception ex)
    {

        return Results.Problem(ex.Message);
    }
});

app.MapPost("/change-password", async (IUserService userService, string email, string oldPassword, string newPassword, CancellationToken cancellationToken) =>
{
    try
    {
        var result = await userService.ChangePasswordAsync(email, oldPassword, newPassword, cancellationToken);
        return Results.Ok(result);
    }
    catch (DomainException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (Exception ex)
    {

        return Results.Problem(ex.Message);
    }

}).RequireAuthorization();

app.Run();


