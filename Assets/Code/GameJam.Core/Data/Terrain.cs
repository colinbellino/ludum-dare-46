using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	[CreateAssetMenu(fileName = "Terrain", menuName = "GameJam/Terrain")]
	public class Terrain : SerializedScriptableObject
	{
		public int Id;
		public Sprite Sprite;
		public bool Unconstructible;
	}
}
