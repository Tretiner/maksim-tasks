using maxim_tasks;
using maxim_tasks.Services.RandomNumberGeneratorService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Custom services
builder.Services.AddSingleton<IRandomNumberGeneratorService, RandomNumberGeneratorService>();
builder.Services.AddSingleton<EvenOddTextReverser>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	//app.UseSwagger();
	//app.UseSwaggerUI();
}

app.MapControllers();

app.Run();