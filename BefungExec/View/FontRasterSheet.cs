using BefungExec.Logic;
using BefungExec.Properties;
using OpenTK.Graphics.OpenGL;
using SuperBitBros.OpenGL.OGLMath;
using System.Drawing;

namespace BefungExec.View.OpenGL
{
	public class FontRasterSheet : OGLTextureSheet
	{
		protected FontRasterSheet(int id, int w, int h)
			: base(id, w, h)
		{

		}

		public static FontRasterSheet create()
		{
			Bitmap b = Resources.raster;

			if (!RunOptions.SYNTAX_HIGHLIGHTING)
			{
				b = b.Clone(new Rectangle(0, 0, b.Width, b.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);

				for (int x = 0; x < b.Width; x++)
				{
					for (int y = 0; y < b.Height; y++)
					{
						Color c = b.GetPixel(x, y);
						if (c.R + c.G + c.B != (255 * 3))
						{
							b.SetPixel(x, y, Color.Black);
						}
					}
				}
			}

			return new FontRasterSheet(LoadResourceIntoUID(b), 80, 2);
		}

		public Rect2d GetCharCoords(int c)
		{
			if (!(c >= 0 && 126 >= c))
			{
				if (c == 164)
					c = 158;
				else
					c = 159;
			}

			return GetCoordinates(c);
		}

		public void Render(Rect2d rect, double distance, int chr)
		{
			Rect2d coords = GetCharCoords(chr);

			//##########
			GL.Begin(BeginMode.Quads);
			//##########

			GL.TexCoord2(coords.bl);
			GL.Vertex3(rect.tl.X, rect.tl.Y, distance);

			GL.TexCoord2(coords.tl);
			GL.Vertex3(rect.bl.X, rect.bl.Y, distance);

			GL.TexCoord2(coords.tr);
			GL.Vertex3(rect.br.X, rect.br.Y, distance);

			GL.TexCoord2(coords.br);
			GL.Vertex3(rect.tr.X, rect.tr.Y, distance);

			//##########
			GL.End();
			//##########
		}
	}
}
