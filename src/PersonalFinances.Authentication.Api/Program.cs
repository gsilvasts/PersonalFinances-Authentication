using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using PersonalFinances.Authentication.Api.Interfaces.Repository;
using PersonalFinances.Authentication.Api.Interfaces.Services;
using PersonalFinances.Authentication.Api.Models;
using PersonalFinances.Authentication.Api.Repositories;
using PersonalFinances.Authentication.Api.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

#region Authn
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthenticationSettings:SecretKey"])),
        ValidIssuer = builder.Configuration["AuthenticationSettings:Issuer"],
        ValidAudience = builder.Configuration["AuthenticationSettings:Audience"]
    };
});
#endregion

#region Authz   
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("manager"));
});
#endregion

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

app.MapPost("/login", async (IUserRepository repository, ITokenService service, string email, string password) =>
{
    var passwordhash = service.ComputerSha256Hash(password);
    var user = await repository.SignInAsync(email, passwordhash);

    if (user is null)
        return Results.NotFound(new { message = "Invalid email or password" });

    var token = service.GenerateJWtToken(user);

    return Results.Ok(new
    {
        user = user,
        token = token,
    });
});

app.MapPost("/signup", async (IUserRepository repository, ITokenService service, UserDto userDto) =>
{
    var user = await repository.GetByEmailAsync(userDto.Email);

    if (user is not null)
        return Results.BadRequest(new { message = "User is exist" });

    var password = service.ComputerSha256Hash(userDto.Password);

    user = new User(userDto.FirstName, userDto.LastName, userDto.Email, password, userDto.Role);

    await repository.InsertAsync(user);

    return Results.Ok(new { user = user });
});

app.MapPost("/update", async (IUserRepository repository, ITokenService service, UpdateUserDto userDto) =>
{
    var user = await repository.GetByIdAsync(userDto.Id);

    if (user is null)
        return Results.BadRequest(new { message = "User is not exist" });

    user.Update(userDto.FirstName, userDto.LastName, userDto.Email, userDto.Role);

    await repository.UpdateAsync(user);

    return Results.Ok(new { user = user });
}).RequireAuthorization();

app.MapPost("/change-password", async (IUserRepository repository, ITokenService service, string email, string oldPassword, string newPassword) =>
{
    var oldPasswordHash = service.ComputerSha256Hash(oldPassword);
    var user = await repository.SignInAsync(email, oldPasswordHash);

    if (user is null)
        return Results.NotFound(new { message = "Invalid email or password" });

    var newPasswordHash = service.ComputerSha256Hash(newPassword);

    await repository.ChangePasswordAsync(user.Id, newPasswordHash);

    return Results.Ok(new { user = user });
}).RequireAuthorization();


app.Run();


internal record UserDto(string FirstName, string LastName, string Email, string Password, string Role);

internal record UpdateUserDto(Guid Id, string FirstName, string LastName, string Email, string Role);

