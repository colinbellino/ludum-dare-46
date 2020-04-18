using UnityEngine;

namespace GameJam.Core
{
	[CreateAssetMenu(fileName = "Structure", menuName = "GameJam/Structure")]
	public class StructureData : ScriptableObject
	{
		public int Id;
		public string Name;
		public Sprite Sprite;
		// public GameObject Model;
	}
}
