using System;
using System.Collections.Generic;
using UnityEngine;

public enum DropRewardType
{
    Money,
    Block
}

[CreateAssetMenu(
    fileName = "DropTable_",
    menuName = "Game/Loot/DropTableDef"
)]
public class DropTableDef : ScriptableObject
{
    [Header("Identity")]
    public string tableId;

    [Header("Entries")]
    public List<DropEntry> entries = new();

    public List<DropResult> Roll() //드랍 결과를 반환
    {
        var result = new List<DropResult>();
        if (entries == null)return result;

        foreach (var e in entries)
        {
            if (e == null || e.item == null) continue;

            if (UnityEngine.Random.value <= e.dropChance)
            {
                int amount = UnityEngine.Random.Range(e.minAmount,e.maxAmount + 1);
                if (amount > 0)
                    result.Add(new DropResult(e.item,amount));
            }

        }
        return result;
    }
}

[Serializable]
public class DropEntry
{
    public DropRewardType rewardType;

    // Money면 무시, Block일 때만 사용
    public string blockId;

    [Range(0f, 1f)]
    public float dropChance = 1f;

    public int minAmount = 1;
    public int maxAmount = 1;
}

public readonly struct DropResult
{
     public readonly DropRewardType rewardType;
    public readonly int amount;
    public readonly string blockId;

    public DropResult(DropRewardType type, int amount, string blockId)
    {
        this.rewardType = type;
        this.amount = amount;
        this.blockId = blockId;
    }
}