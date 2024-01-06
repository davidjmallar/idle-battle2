using UnityEngine;

public abstract class GameListElement<T> : MonoBehaviour
{
	public abstract void Setup(T data);
}
