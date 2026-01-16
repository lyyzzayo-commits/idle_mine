using UnityEngine;

[CreateAssetMenu(
    fileName = "BlockDef_",
    menuName = "Game/Block/BlockDef"
)]
public class BlockDef : ScriptableObject
{
    [Header("Identity")]
    public string blockId;            // "Dirt", "Stone" 등
    public string displayName;

    [Header("Stats")]
    public int maxHP;                 // 블록 체력
    public int hardness;              // 요구 채굴력(미만이면 데미지 감소/불가 처리)

    [Header("Economy")]
    public int baseValue;             // 즉시 판매 시 골드 가치
    public DropTableDef dropTable;     // 파괴 시 드랍(선택)

    [Header("Visual")]
    public GameObject blockPrefab;    // 타일/프리팹


}
