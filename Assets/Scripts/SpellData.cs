
public class SpellData
{
	public string SpellId;
	public string SpellIconId;
	public string DisplayName;
	public string DescriptionTemplate;

	public bool IsTargetingEnemy;
	public float Periodicity;
	public int Range;
	public int EffectArea;

	public int MgtMultiplier;
	public int AgiMultiplier;
	public int FocMultiplier;

	// Threat
	// Dam/Heal? -> need creature too
}
