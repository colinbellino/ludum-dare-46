using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class TerrainComponent : MonoBehaviour
	{
		[SerializeField] [Required] private SpriteRenderer _renderer;

		public void Initialize(Terrain data)
		{
			name = $"Terrain ({data.Id})";

			_renderer.sprite = data.Sprite;

			if (data.Unconstructible)
			{
				var fertile = gameObject.AddComponent<UnconstructibleFlag>();
			}
		}
	}
}
