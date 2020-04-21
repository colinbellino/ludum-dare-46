using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class TerrainComponent : MonoBehaviour
	{
		[SerializeField] [Required] private SpriteRenderer _renderer;

		public int Id { get; private set; }

		public void Initialize(Terrain data)
		{
			name = $"Terrain ({data.Id})";
			Id = data.Id;

			_renderer.sprite = data.Sprite;
		}
	}
}
