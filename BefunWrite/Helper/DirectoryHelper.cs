
namespace BefunWrite.Helper
{
	public static class DirectoryHelper
	{
		public static string PrepareStringAsPath(string filename)
		{
			foreach (char c in System.IO.Path.GetInvalidFileNameChars())
				filename = filename.Replace(c, '_');

			foreach (char c in System.IO.Path.GetInvalidPathChars())
				filename = filename.Replace(c, '_');

			return filename;
		}
	}
}
