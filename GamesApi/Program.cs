using GamesApi.Services;
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
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        conf.CreateMap<GameDto, Game>()
        .ForMember(game => game.GameGenres,
            opt => opt.MapFrom(dto => dto.GameGenres.ToHashSet()))
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        conf.CreateMap<UpdateGameDto, Game>()
        .ForMember(game => game.GameGenres,
            opt => opt.MapFrom(dto => dto.GameGenres.ToHashSet()))
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        conf.CreateMap<StudioDeveloper, StudioDeveloper>()
        .ForMember(old => old.Id, opt => opt.Ignore())
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;

        conf.CreateMap<StudioDeveloperDto, StudioDeveloper>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        conf.CreateMap<UpdateStudioDeveloperDto, StudioDeveloper>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    });

var cS = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")??
    "host=localhost;port=5432;database=games;username=postgres;password=abrakadabra77";

builder.Services
    .AddEntityFrameworkNpgsql()
    .AddDbContext<GameContext>(opt => opt.UseNpgsql(cS));

builder.Services.AddScoped<GameContext>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IDeveloperRepository, DeveloperRepository>();
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<DeveloperService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

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