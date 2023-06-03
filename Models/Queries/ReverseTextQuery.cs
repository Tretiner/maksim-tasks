using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace maxim_tasks.Models.Queries;

public sealed record ReverseTextQuery(
	[Required]
	string Text,
	[Optional]
	char Sorter
);
