
namespace BefunRep
{
	public static class CustomExtensions
	{
		public static void Fill<T>(this T[] originalArray, T with)
		{
			for (int i = 0; i < originalArray.Length; i++)
			{
				originalArray[i] = with;
			}
		}
	}
}
