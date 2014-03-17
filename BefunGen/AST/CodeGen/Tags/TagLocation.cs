
using System;
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

		public override string ToString()
		{
			return String.Format(@"({0}|{1}) {2}", X, Y, base.ToString());
		}
	}
}
