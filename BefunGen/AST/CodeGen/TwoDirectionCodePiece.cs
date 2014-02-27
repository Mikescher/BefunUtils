using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.CodeGen
{
	public class TwoDirectionCodePiece
	{
		private Tuple<CodePiece, CodePiece> content;

		public CodePiece LeftToRight { get { return content.Item1; } }
		public CodePiece RightToLeft { get { return content.Item2; } }

		public CodePiece Normal { get { return content.Item1; } }
		public CodePiece Reversed { get { return content.Item2; } }

		public int Width_L2R { get { return content.Item1.Width; } }
		public int Width_R2L { get { return content.Item2.Width; } }

		public int Width_Normal { get { return content.Item1.Width; } }
		public int Width_Reversed { get { return content.Item2.Width; } }

		public int MaxWidth { get { return Math.Max(content.Item1.Width, content.Item2.Width); } }

		public  TwoDirectionCodePiece(CodePiece norm, CodePiece rev)
		{
			content = Tuple.Create(norm, rev);
		}

		public TwoDirectionCodePiece()
			: this(new CodePiece(), new CodePiece())
		{
		}
	}
}
