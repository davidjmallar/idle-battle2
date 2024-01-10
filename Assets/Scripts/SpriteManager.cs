using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance { get; private set; }
    private void Awake() => Instance = this;

    [Required]
	public SpriteAtlas Atlas;
    [Required]
    public SpriteAtlas IconAtlas;

	public Sprite GetAnimationFrame(string spriteId, int animationIndex)
	{
			var x = Atlas.GetSprite($"sprite_sheet_{spriteId}_0_16x16_{animationIndex}");
		return x;
	}

	public Sprite GetAvatar16(string spriteId) => IconAtlas.GetSprite($"icon_{spriteId}_0_16x16");
	public Sprite GetAvatar32(string spriteId) => IconAtlas.GetSprite($"icon_{spriteId}_0_32x32");
}
