using System;
using System.Diagnostics;
using GamesApi.DB;
using GamesApi.DB.Repositories;
using GamesApi.Models;
using GamesApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(confg =>
    confg.SerializerSettings.ReferenceLoopHandling =
    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddAutoMapper(conf =>
    {
        conf.AllowNullCollections = true;

        conf.CreateMap<Game, Game>()
        .ForMember(old=>old.Id, opt=>opt.Ignore())
         .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;

        conf.CreateMap<GameDto, Game>()
        .ForMember(game => game.GameGenres,
            opt => opt.MapFrom(dto => dto.GameGenres.ToHashSet()))
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        conf.CreateMap<StudioDeveloperDto, StudioDeveloper>();
    });

var cS = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")??
    "host=localhost;port=5432;database=games;username=postgres;password=abrakadabra77";

builder.Services
    .AddEntityFrameworkNpgsql()
    .AddDbContext<GameContext>(opt => opt.UseNpgsql(cS));

builder.Services.AddScoped<GameContext>();
builder.Services.AddScoped<GameRepository, MyGameRepository>();
builder.Services.AddScoped<DevelopersRepository, MyDevelopersRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Prepare(app);

app.Run();


static void Prepare(IApplicationBuilder app)
{
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    using (var context = app.ApplicationServices.CreateScope())
    {
        var supportContext = context.ServiceProvider.GetService<GameContext>();
        supportContext.Database.Migrate();
    }
}