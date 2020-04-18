using UnityEngine;

namespace GameJam.Core
{
	[CreateAssetMenu(fileName = "Terrain", menuName = "GameJam/Terrain")]
	public class Terrain : ScriptableObject
	{
		public int Id;
		public Sprite Sprite;
	}
}
