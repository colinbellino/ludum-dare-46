using System;

namespace GameJam.Core
{
	[Serializable]
	public class Cell
	{
		public int Terrain = -1;
		public int Structure = -1;
		public int Fire;
	}
}
