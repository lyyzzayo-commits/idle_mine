using UnityEngine;

public enum PickaxeTier
{
    Wood,
    Stone,
    Iron,
    Gold,
    Diamond
}
[CreateAssetMenu(
    fileName = "PickaxeTierVisual_",
    menuName = "Game/Pickaxe/PickaxeTierVisualDef"
)]
public class PickaxeTierVisualDef : ScriptableObject
{
    [Header("Identity")]
    public PickaxeTier tier;
    public string displayName;

    [Header("Visual")]
    public GameObject pickaxePrefab;
    public Sprite icon;
}