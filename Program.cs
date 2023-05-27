//var builder = WebApplication.CreateBuilder(args);

//var app = builder.Build();

//app.Run();


using maxim_tasks;

Console.WriteLine("Enter the text: ");

var text = Console.ReadLine();

var result = EvenOddTextReverser.ReverseText(text);

Console.WriteLine(result);