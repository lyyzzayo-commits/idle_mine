using System;

[Serializable] 
public class PickaxeProgress
{
    public string pickaxeId;       // 곡괭이 고유 ID
    public string tierId = "Wood"; // 현재 티어
    public int level = 1;          // 티어 내부 레벨

    public int durability = 100;   // 내구도 (선택)

    public static PickaxeProgress CreateNew(string startTierId = "Wood")
    {
        return new PickaxeProgress
        {
            pickaxeId = Guid.NewGuid().ToString("N"),
            tierId = startTierId,
            level = 1,
            durability = 100
        };
    }
}
