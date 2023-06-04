using maxim_tasks.Services.EvenOddTextReverserService;
using maxim_tasks.Services.RandomNumberGeneratorService;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

// Custom services
builder.Services
	.AddSingleton<IRandomNumberGeneratorService, RandomNumberGeneratorService>()
	.AddSingleton<IEvenOddTextReverserService, EvenOddTextReverserService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

app.Run();