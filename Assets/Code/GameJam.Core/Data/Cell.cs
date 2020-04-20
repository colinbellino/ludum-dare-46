using System;
using Sirenix.OdinInspector;

namespace GameJam.Core
{
	[Serializable]
	public class Cell
	{
		[HorizontalGroup("split")]
		[VerticalGroup("split/a"), LabelWidth(50)]
		public int Terrain = -1;

		[VerticalGroup("split/b"), LabelWidth(50)]
		public int Structure = -1;

		[VerticalGroup("split/c"), LabelWidth(30)]
		public int Fire;
	}
}
