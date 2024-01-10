using Sirenix.OdinInspector;
using UnityEngine;

public class HeroMenuController : MonoBehaviour
{
	public static HeroMenuController Instance { get; private set; }
	private void Awake() => Instance = this;

	[Required] public HeroFormationController FormationController;
	[Required] public HeroDetailsController DetailsController;

}