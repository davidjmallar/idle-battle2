using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSpawnerManager : MonoBehaviour
{
	public static TileSpawnerManager Instance { get; private set; }
	private void Awake() => Instance = this;

	public Tilemap GroundLevel;
	public List<TileBase> TestGroundBase;

	private int IndexOffset = -((GlobalConstants.MapWidth / 2) + 1);
	private int CleanupDistance = -(GlobalConstants.MapWidth * 2);

	[HideInInspector]
	public int ProgressionIndex;

	[Button]
	public void Restart()
	{
		ProgressionIndex = 0;
		GroundLevel.ClearAllTiles();
	}

	[Button]
	public void Progress()
	{

		for (int i = -10; i < 10; i++)
		{
			GroundLevel.SetTile(new Vector3Int(ProgressionIndex + IndexOffset, i, 0), TestGroundBase.PickRandom());
			GroundLevel.SetTile(new Vector3Int(ProgressionIndex + IndexOffset + CleanupDistance, i, 0), null);
		}

		ProgressionIndex++;
	}
}