//var builder = WebApplication.CreateBuilder(args);

//var app = builder.Build();

//app.Run();


using maxim_tasks;

Console.WriteLine("¬ведите строку: ");

var text = Console.ReadLine()!;

var result = Utils.EvenOddReverseText(text);

Console.WriteLine($"–езультат:\n{text} -> {result}");