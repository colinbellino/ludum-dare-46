using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Core
{
	public class MainMenu : MonoBehaviour
	{
		public void Awake()
		{
#if UNITY_WEBGL
			GameObject.Find("Exit Button")?.SetActive(false);
#endif
		}
	}
}
