using UnityEngine;

[CreateAssetMenu(menuName = "Defs/Pickaxe Tier Trigger")]
public sealed class PickaxeTierDef : ScriptableObject
{
    [SerializeField] private string fromTierId;
    [SerializeField] private string toTierId;
    [SerializeField] private int triggerLevel; // 10, 20, 40
    [SerializeField] private Sprite nextTierSprite;

    public string FromTierId => fromTierId;
    public string ToTierId => toTierId;

    public bool IsTriggered(int oldLevel, int newLevel)
    {
        return oldLevel < triggerLevel && newLevel >= triggerLevel;
    }
}
