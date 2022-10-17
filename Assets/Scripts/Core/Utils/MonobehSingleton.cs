using UnityEngine;

namespace Core.Utils
{
	public abstract class MonobehSingleton<T> : MonoBehaviour where T : MonobehSingleton<T>
	{
		private static T _instance;
		private bool _isInited;

		public static T Instance
		{
			get
			{
				if (!_instance)
				{
					_instance = GameObject.FindObjectOfType<T>();
					if (_instance != null) return _instance;

					var sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCount;
					for (int i = 0; i < sceneCount; i++)
					{
						var scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
						var allGameObjects = scene.GetRootGameObjects();
						for (int j = 0; j < allGameObjects.Length; j++)
						{
							var go = allGameObjects[j];
							_instance = go.GetComponentInChildren<T>(true);
							if (_instance)
							{
								if (!_instance._isInited)
								{
									_instance.Init();
									_instance._isInited = true;
								}
								return _instance;
							}
						}
					}
				}
				return _instance;
			}
		}

		protected virtual void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Debug.LogWarningFormat("MonobehSingleton({0}) already created!", typeof(T));
			}
			if (!_isInited)
			{
				Init();
				_isInited = true;
			}
			_instance = this as T;
		}

		protected virtual void Init() { }
		protected virtual void OnDestroy()
		{
			if (_instance == this) _instance = null;
		}
	}
}
