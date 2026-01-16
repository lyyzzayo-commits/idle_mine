using UnityEngine;

public static class SaveSystem
{
    private const string LevelKey = "Pickaxe_Level";
    private const string PickIdKey = "Pickaxe_Id";

    public static PickaxeProgress LoadPickaxeProgress()
    {
        if (!PlayerPrefs.HasKey(LevelKey))
            return null;

        return new PickaxeProgress
        {
            level = PlayerPrefs.GetInt(LevelKey, 1),
            pickaxeId = PlayerPrefs.GetString(PickIdKey, "Pick_01")
        };
    }

    public static void SavePickaxeProgress(PickaxeProgress progress)
    {
        PlayerPrefs.SetInt(LevelKey, progress.level);
        PlayerPrefs.SetString(PickIdKey, progress.pickaxeId);
        PlayerPrefs.Save();
    }
}
