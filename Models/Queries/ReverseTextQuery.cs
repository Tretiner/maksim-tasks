using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.InteropServices;

namespace maxim_tasks.Models.Queries;

/// <param name="Text">String which needs to be processed</param>
/// <param name="Sorter">Sorter option [q: QuickSort, t: TreeSort] default:QuickSort</param>
public sealed record ReverseTextQuery(
	[BindRequired]
	string Text,
	[Optional]
	char Sorter
);
