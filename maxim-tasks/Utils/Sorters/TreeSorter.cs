namespace maxim_tasks.Utils.Sorters;

public sealed class TreeNode
{
	public TreeNode(char data, TreeNode? left = null, TreeNode? right = default)
	{
		Data = data;
		Left = left;
		Right = right;
	}

	public char Data { get; set; }
	public TreeNode? Left { get; set; }
	public TreeNode? Right { get; set; }

	public void Insert(TreeNode node)
	{
		if (node.Data < Data)
		{
			if (Left == null)
			{
				Left = node;
			}
			else
			{
				Left.Insert(node);
			}
		}
		else
		{
			if (Right == null)
			{
				Right = node;
			}
			else
			{
				Right.Insert(node);
			}
		}
	}


	public Span<char> ToArray(List<char>? elements = null)
	{
		elements ??= new List<char>();

		Left?.ToArray(elements);

		elements.Add(Data);

		Right?.ToArray(elements);

		return elements.ToArray();
	}
}

public class TreeSorter : IStringSorter
{
	public string SortString(string text)
	{
		ArgumentNullException.ThrowIfNull(text);

		if (string.IsNullOrWhiteSpace(text))
		{
			return text;
		}

		Span<char> sp = text.ToCharArray().AsSpan();
		return TreeSort(sp).ToString();
	}

	private static Span<char> TreeSort(Span<char> arr)
	{
		var treeNode = new TreeNode(arr[0]);
		for (int i = 1; i < arr.Length; i++)
		{
			treeNode.Insert(new TreeNode(arr[i]));
		}

		return treeNode.ToArray();
	}
}
