using Sirenix.OdinInspector;
using UnityEngine;

namespace GameJam.Core
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] [Required] private SpriteRenderer _renderer;

		public CellContent Content { get; private set; }
		public Sprite Sprite { get; private set; }

		public void Initialize(CellAuthoring authoringData)
		{
			Content = SpawnContent(authoringData.Content);
			_renderer.sprite = Resources.Load<Sprite>($"Art/Sprites/CellType{authoringData.Type}");
		}

		public void SetContent(int contentId)
		{
			if (Content)
			{
				DestroyImmediate(Content);
			}

			Content = SpawnContent(contentId);
		}

		private CellContent SpawnContent(int contentId)
		{
			var prefab = Resources.Load<GameObject>("Prefabs/CellContent");

			var instance = Instantiate(prefab, transform);
			instance.transform.parent = transform;
			var content = instance.GetComponent<CellContent>();
			content.Initialize(contentId);

			return content;
		}
	}
}
