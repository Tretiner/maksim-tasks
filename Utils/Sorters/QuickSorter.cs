namespace maxim_tasks.Utils.Sorters;

public class QuickSorter : IStringSorter
{
	public string SortString(string text)
	{
		Span<char> sp = text.ToCharArray().AsSpan();
		return QuickSort(sp).ToString();
	}

	private static Span<char> QuickSort(Span<char> array)
	{
		QuickSort(array, 0, array.Length - 1);
		return array;
	}

	private static void QuickSort(Span<char> array, int start, int end)
	{
		if (start >= end)
		{
			return;
		}

		var pivot = array[start];
		var i = start;
		var j = end;

		while (i <= j)
		{
			while (array[i] < pivot)
			{
				i++;
			}

			while (array[j] > pivot)
			{
				j--;
			}

			if (i <= j)
			{
				(array[i], array[j]) = (array[j], array[i]);
				i++;
				j--;
			}
		}

		if (start < j)
			QuickSort(array, start, j);

		if (i < end)
			QuickSort(array, i, end);

		return;
	}
}
