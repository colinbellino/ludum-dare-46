using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class StructureComponent : MonoBehaviour
	{
		[SerializeField] [Required] private SpriteRenderer _renderer;

		public bool IsActive { get; private set; }
		public int Id { get; private set; }

		public void Initialize(Structure data, Vector2Int position)
		{
			PlaceStructure(data, position);

			if (data.IsExit)
			{
				var exit = gameObject.AddComponent<ExitFlag>();
				gameObject.AddComponent<IndestructibleOnFireFlag>();
			}

			if (data.Unburnable)
			{
				var unburnable = gameObject.AddComponent<UnburnableFlag>();
			}
		}

		public void PlaceStructure(Structure data, Vector2Int position)
		{
			SetData(data);
			IsActive = true;
			_renderer.sortingOrder = -position.x;
		}

		public void DestroyStructure()
		{
			SetData(null);
			IsActive = false;
		}

		private void SetData(Structure data)
		{
			name = $"Structure ({data?.Name})";
			Id = data ? data.Id : -1;

			if (data?.Sprite)
			{
				_renderer.sprite = data.Sprite;
			}
			else
			{
				// _renderer.sprite = null;
			}
		}
	}
}
