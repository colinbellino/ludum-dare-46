using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] [Required] private SpriteRenderer _renderer;

		public CellContent Content { get; private set; }

		public void Initialize(CellAuthoring authoringData)
		{
			if (authoringData.Content > -1)
			{
				Content = SpawnContent(authoringData.Content);
			}

			_renderer.sprite = Resources.Load<Sprite>($"Art/Sprites/CellType{authoringData.Type}");
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

		private CellContent SpawnContent(int contentId)
		{
			var prefab = Resources.Load<GameObject>("Prefabs/CellContent");

			var instance = Instantiate(prefab, transform);
			instance.name = $"Content ({contentId})";

			var content = instance.GetComponent<CellContent>();
			content.Initialize(contentId);

			return content;
		}
	}
}
