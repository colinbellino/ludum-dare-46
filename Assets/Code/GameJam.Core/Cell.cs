using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] [Required] private SpriteRenderer _renderer;

		public Structure Content { get; private set; }

		public void Initialize(CellAuthoring data)
		{
			if (data.Content > -1)
			{
				Content = SpawnContent(data.Content);
				Content.SetFire(data.Fire);
			}

			_renderer.sprite = Resources.Load<Sprite>($"Art/Sprites/CellType{data.Type}");
		}

		public void SetContent(int contentId)
		{
			if (Content)
			{
				Destroy(Content.gameObject);
				Content = null;
			}

			if (contentId > -1)
			{
				Content = SpawnContent(contentId);
			}
		}

		private Structure SpawnContent(int contentId)
		{
			var prefab = Resources.Load<GameObject>("Prefabs/Structure");

			var instance = Instantiate(prefab, transform);
			instance.name = $"Content ({contentId})";

			var content = instance.GetComponent<Structure>();
			content.Initialize(contentId);

			return content;
		}
	}
}
