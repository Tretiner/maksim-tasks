namespace maxim_tasks.Models.Responses;

internal sealed record RandomNumberResponse(
	ResultPartResponse Result
);

internal sealed record ResultPartResponse(
	RandomPartResponse Random
);

internal sealed record RandomPartResponse(
	int[] Data
);
