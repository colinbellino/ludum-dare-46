using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	[CreateAssetMenu(fileName = "Structure", menuName = "GameJam/Structure")]
	public class Structure : SerializedScriptableObject
	{
		public int Id;
		public string Name;
		public Sprite Sprite;
		public bool IsExit;
		public bool Unburnable;
	}
}
