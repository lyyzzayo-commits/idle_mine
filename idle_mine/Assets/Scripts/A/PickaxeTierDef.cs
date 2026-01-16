using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/Pickaxe Progression")]
public sealed class PickaxeProgressionDef : ScriptableObject
{
    [Header("Level Stats (performance)")]
    public float baseMineSpeed = 1f;
    public float mineSpeedPerLevel = 0.05f;

    public int baseMiningPower = 1;
    public int miningPowerPerLevel = 1;

    public int baseRange = 1;
    public int rangeEveryNLevels = 0; // 0 = 고정

    public float baseIncomeBonus = 0f;
    public float incomeBonusPerLevel = 0.01f;

    public int baseAutomationLevel = 0;
    public int automationEveryNLevels = 0;

    [Header("Tier transitions (visual only)")]
    public List<TierStep> tierSteps = new(); // level -> tier

    public PickaxeStats EvalStats(int level)
    {
        level = Mathf.Max(1, level);

        int range = baseRange + (rangeEveryNLevels > 0 ? (level - 1) / rangeEveryNLevels : 0);
        int autoLv = baseAutomationLevel + (automationEveryNLevels > 0 ? (level - 1) / automationEveryNLevels : 0);

        return new PickaxeStats(
            mineSpeed: baseMineSpeed * (1f + mineSpeedPerLevel * (level - 1)),
            miningPower: baseMiningPower + miningPowerPerLevel * (level - 1),
            range: range,
            incomeBonus: baseIncomeBonus + incomeBonusPerLevel * (level - 1),
            automationLevel: autoLv
        );
    }

    public PickaxeTier GetTierForLevel(int level)
    {
        level = Mathf.Max(1, level);

        PickaxeTier current = tierSteps.Count > 0 ? tierSteps[0].tier : PickaxeTier.Wood;
        for (int i = 0; i < tierSteps.Count; i++)
        {
            if (level >= tierSteps[i].minLevel)
                current = tierSteps[i].tier;
        }
        return current;
    }

    public bool IsTierUpTriggered(int oldLevel, int newLevel, out PickaxeTier newTier)
    {
        var oldTier = GetTierForLevel(oldLevel);
        newTier = GetTierForLevel(newLevel);
        return newTier != oldTier;
    }
}

[Serializable]
public class TierStep
{
    public PickaxeTier tier;
    public int minLevel; // 이 레벨 이상이면 해당 tier
}

public readonly struct PickaxeStats
{
    public readonly float mineSpeed;
    public readonly int miningPower;
    public readonly int range;
    public readonly float incomeBonus;
    public readonly int automationLevel;

    public PickaxeStats(float mineSpeed, int miningPower, int range, float incomeBonus, int automationLevel)
    {
        this.mineSpeed = mineSpeed;
        this.miningPower = miningPower;
        this.range = range;
        this.incomeBonus = incomeBonus;
        this.automationLevel = automationLevel;
    }
}
