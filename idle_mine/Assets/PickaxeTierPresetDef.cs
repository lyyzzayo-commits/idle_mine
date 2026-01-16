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
    fileName = "PickaxeTier_",
    menuName = "Game/Pickaxe/PickaxeTierPreset"
)]
public class PickaxeTierPresetDef : ScriptableObject
{
    [Header("Identity")]
    public PickaxeTier tier;
    public string displayName;

    [Header("Base Stats")]
    public float mineSpeed;        // 채굴 속도 (타격 간격 or 초당 타격 수)
    public int miningPower;        // 채굴력 (hardness 비교용)
    public int range;              // 범위 (1 = 단일 블록)

    [Header("Economy")]
    public float incomeBonus;      // 수익 보너스 (1.0 = 100%)

    [Header("Automation")]
    public int automationLevel;    // 기본 자동화 레벨 (0 = 수동)

    [Header("Visual")]
    public GameObject pickaxePrefab;
    public Sprite icon;
}