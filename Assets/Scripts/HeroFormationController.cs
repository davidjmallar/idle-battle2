using Assets.Scripts;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroFormationController : MonoBehaviour
{

	[Required, FoldoutGroup("Templates"), AssetsOnly]
	public HeroAvatarInFormationController AvatarTemplate;

	[Required]
	public Button AddButtonTemplate;


	private SaveData Data => DataService.GetSaveData();



	// Start is called before the first frame update
	void Start()
    {
        Debug.Log("OnStart");
	}

	private void OnEnable()
	{
		Debug.Log("OnEnable");
	}

	private void OnDisable()
	{
        Debug.Log("OnDisable");
	}
	private void OnDestroy()
	{
        Debug.Log("OnDestroy");
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
