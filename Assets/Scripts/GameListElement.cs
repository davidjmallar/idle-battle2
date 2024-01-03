using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameListElement<T> : MonoBehaviour
{
	public abstract IEnumerable<T> FullList();
	public abstract void Setup(T data);

}
