//var builder = WebApplication.CreateBuilder(args);

//var app = builder.Build();

//app.Run();


using maxim_tasks;
using maxim_tasks.Utils.Sorters;

Console.WriteLine("Enter the text: ");

var text = Console.ReadLine();

Console.WriteLine(
@"Enter preffered sort algorithm (default: QuickSort):
  't' -> TreeSort
  'q' -> QuickSort");

var input = Console.ReadLine()?.Trim();
IStringSorter sorter = input switch
{
	"q" => new QuickSorter(),
	"t" => new TreeSorter(),
	_ => new QuickSorter()
};

var result = EvenOddTextReverser.ReverseText(text, sorter);

Console.WriteLine(result);