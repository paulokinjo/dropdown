using backend.Settings;
using Bogus;
using Data;
using Filters;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Contracts;

const string AllowAllOrigins = "AllowAllOrigins";

var builder = WebApplication.CreateBuilder(args);

// Bind AppSettings to the configuration section
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings.Api)));

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("PeopleDb"));
builder.Services.AddScoped<IPersonContract, PersonService>();

var customHeaderKey = builder.Configuration.GetSection($"{nameof(AppSettings.Api)}:{nameof(AppSettings.Api.CustomHeader)}");
builder.Services.AddControllers(options =>
{
    string headerName = customHeaderKey.GetSection(nameof(CustomHeaderSettings.Key)).Value;
    string headerValue = customHeaderKey.GetSection(nameof(CustomHeaderSettings.Value)).Value;
    options.Filters.Add(new HeaderValidationFilter(headerName, headerValue));
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowAllOrigins, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(AllowAllOrigins);

app.UseHttpsRedirection();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!context.People.Any())
    {
        // Create a Faker instance for Person
        var personFaker = new Faker<Domain.Entities.Person>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.GivenName, f => f.Name.FirstName())
            .RuleFor(p => p.Surname, f => f.Name.LastName())
            .RuleFor(p => p.Gender, f => f.PickRandom<Domain.Entities.Gender>())
            .RuleFor(p => p.BirthDate, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
            .RuleFor(p => p.BirthLocation, f => f.Address.City())
            .RuleFor(p => p.DeathDate, f => f.Random.Bool(0.4f) ? f.Date.Past(10, DateTime.Now) : (DateTime?)null)
            .RuleFor(p => p.DeathLocation, (f, p) => p.DeathDate != null ? f.Address.City() : null);

        // Generate 50 people 
        List<Domain.Entities.Person> people = personFaker.Generate(50);

        people.Take(3).ToList().ForEach(p => p.BirthDate = default);
        people.Skip(3).Take(1).ToList().ForEach(p =>
        {
            p.BirthDate = DateTime.Now.AddYears(-120);
            p.DeathDate = default;
        });

        context.People.AddRange(people);
        context.SaveChanges();
    }
}

app.Run();
