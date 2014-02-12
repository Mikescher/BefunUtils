
namespace BefunGen.AST.CodeGen
{
	public class SourceCodePosition
	{
		public readonly int Line;
		public readonly int Column;

		public SourceCodePosition()
		{
			this.Line = -1;
			this.Column = -1;
		}

		public SourceCodePosition(int l, int c)
		{
			this.Line = l + 1;
			this.Column = c;
		}

		public SourceCodePosition(GOLD.Position p)
		{
			this.Line = p.Line + 1;
			this.Column = p.Column;
		}

		public SourceCodePosition(GOLD.Parser p)
		{
			this.Line = p.CurrentPosition().Line + 1;
			this.Column = p.CurrentPosition().Column;
		}

		public override string ToString()
		{
			return Line + ":" + Column;
		}
	}
}
