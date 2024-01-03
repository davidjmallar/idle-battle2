using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance { get; private set; }
    private void Awake() => Instance = this;

    [Required]
    public SpriteAtlas Atlas;

	public Sprite GetAnimationFrame(string spriteId, int animationIndex) => Atlas.GetSprite($"sprite_sheet_{spriteId}_0_16x16_{animationIndex}");
	public Sprite GetAvatar16(string spriteId) => Atlas.GetSprite($"icon_{spriteId}_16x16");
	public Sprite GetAvatar32(string spriteId) => Atlas.GetSprite($"icon_{spriteId}_32x32");
}
