using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T: MonoBehaviour
{
	private static T _instance;
	public static T Instance
	{
		get
		{
			if (_instance == null) // instance is not assigned
			{
				_instance = FindObjectOfType<T>();
				if (_instance == null) // couldn't find instance on scene
				{
					GameObject newGameObject = new GameObject(typeof(T).Name);
					_instance = newGameObject.AddComponent<T>();
				}
			}
			return _instance;
		}
	}

	protected virtual void Awake()
	{
		_instance = this as T;
	}
}
