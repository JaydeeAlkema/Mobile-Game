using NaughtyAttributes;
using UnityEngine;

namespace MobileGame.Managers
{
	public class RuntimeManager : MonoBehaviour
	{
		private static RuntimeManager instance;
		public static RuntimeManager Instance
		{
			get
			{
				if (instance == null)
					instance = FindFirstObjectByType<RuntimeManager>();

				return instance;
			}
		}

		[SerializeField, BoxGroup("Game Settings")] private int targetFrameRate = 60;

		private void Awake()
		{
			SetInstance();

			Application.targetFrameRate = targetFrameRate;
		}

		private void SetInstance()
		{
			if (instance == null)
				instance = this;
			else
				Destroy(gameObject);
		}
	}
}
