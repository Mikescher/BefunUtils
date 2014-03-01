using BefunGen.MathExtensions;
using OpenTK.Input;
using System.Collections.Generic;

namespace BefungExec.View
{
	public class IdleKeyBoard
	{
		private Dictionary<Key, bool> last = new Dictionary<Key, bool>();
		private Dictionary<Key, bool> now = new Dictionary<Key, bool>();

		public IdleKeyBoard()
		{

		}

		public bool this[Key k]
		{
			get
			{
				if (last.ContainsKey(k))
				{
					return now[k] && !last[k];
				}
				else
				{
					now.Add(k, false);
					return false;
				}
			}
		}

		public void update(KeyboardDevice k)
		{
			MathExt.Swap(ref last, ref now);
			now.Clear();

			foreach (KeyValuePair<Key, bool> kvp in last)
			{
				now.Add(kvp.Key, k[kvp.Key]);
			}
		}
	}
}
