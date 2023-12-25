using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
	private Camera _camera;

	//public float TileHeight { get; private set; }
	public int TileWidth => GlobalConstants.MapWidth;

	//private static CameraController _instance;
	//public static CameraController Instance => _instance;

	//public static float UnitSize { get; private set; }

	//public Transform PlayerGroup;

	//private void Awake()
	//{
	//	UnitSize = Mathf.CeilToInt((float)Screen.width / (float)TileWidth);
	//	_instance = this;
	//}

	private void Start()
	{
		_camera = GetComponent<Camera>();
		_camera.orthographicSize = TileWidth / 2f / _camera.aspect;
		//TileHeight = _camera.orthographicSize * 2f;
		//_camera.transform.position = new Vector3(_camera.transform.position.x, /*_camera.aspect * 2f*/ 0, _camera.transform.position.z);
	}
}
