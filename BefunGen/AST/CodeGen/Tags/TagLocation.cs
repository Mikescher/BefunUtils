
namespace BefunGen.AST.CodeGen.Tags
{
	public class TagLocation
	{
		public readonly CodeTag Tag;

		public readonly int X;
		public readonly int Y;

		public readonly BefungeCommand Command;

		public TagLocation(int px, int py, BefungeCommand cmd)
		{
			Tag = cmd.Tag;
			X = px;
			Y = py;
			Command = cmd;
		}
	}
}
