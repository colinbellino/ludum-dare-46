using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class StructureComponent : MonoBehaviour
	{
		[SerializeField] [Required] private SpriteRenderer _renderer;

		public bool IsActive { get; private set; }

		public void Initialize(Structure data)
		{
			PlaceStructure(data);

			if (data.IsExit)
			{
				var exit = gameObject.AddComponent<ExitFlag>();
			}
		}

		public void PlaceStructure(Structure data)
		{
			SetData(data);
			IsActive = true;
		}

		public void DestroyStructure()
		{
			SetData(null);
			IsActive = false;
		}

		private void SetData(Structure data)
		{
			name = $"Structure ({data?.Name})";

			if (data?.Sprite)
			{
				_renderer.sprite = data.Sprite;
			}
			else
			{
				_renderer.sprite = null;
			}
		}
	}
}
