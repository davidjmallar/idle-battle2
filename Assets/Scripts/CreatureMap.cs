using System.Collections.Generic;
using UnityEngine;

public static class CreatureMap
{
	public static List<CreatureController> Heroes { get; } = new List<CreatureController>();
	public static List<CreatureController> Foes { get; } = new List<CreatureController>();
	public static void ResetMap()
	{
		Heroes.ForEach(c => GameObject.Destroy(c.gameObject));
		Foes.ForEach(c => GameObject.Destroy(c.gameObject));
		Heroes.Clear();
		Foes.Clear();
	}
}
