using UnityEditor;
using UnityEngine;

public class PickManager : MonoBehaviour
{
    [SerializeField] private PickaxeTierDef[] tierDefs;

    public void TryLevelUp(PickaxeProgress progress)
    {
        if (progress == null)
            return;

        int oldLevel = progress.level;
        int newLevel = oldLevel + 1;

        PickaxeTierDef tierDef = FindTierDef(progress.tierId);
        if (tierDef != null && tierDef.IsTriggered(oldLevel, newLevel))
            progress.tierId = tierDef.ToTierId;
            GameSignals.RaiseTierChanged();

        progress.level = newLevel;
        GameSignals.RaiseLevelChanged();

        //pickaxeUnit.ApplyLevel(nextLevelDef)
    }

    private PickaxeTierDef FindTierDef(string tierId)
    {
        if (tierDefs == null)
            return null;

        foreach (PickaxeTierDef tierDef in tierDefs)
        {
            if (tierDef != null && tierDef.FromTierId == tierId)
                return tierDef;
        }

        return null;
    }
}
