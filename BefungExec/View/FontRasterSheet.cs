using BefungExec.Properties;
using OpenTK.Graphics.OpenGL;
using SuperBitBros.OpenGL.OGLMath;

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
			return new FontRasterSheet(LoadResourceIntoUID(Resources.raster), 80, 2);
		}

		public Rect2d GetCharCoords(int c)
		{
			if (!(c >= 32 && 126 >= c))
			{
				c = 0;
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
