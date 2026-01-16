using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(
    fileName = "GameDatabase",
    menuName = "Game/Data/GameDatabase"
)]
public class GameDatabase : ScriptableObject
{
    [Header("Blocks")]
    [SerializeField] private List<BlockDef> blockDefs = new();

   

    [Header("Pickaxe Tiers")]
    [SerializeField] private List<PickaxeTierPresetDef> pickaxeTierPresets = new();

    [Header("Curves")]
    [SerializeField] private List<UpgradeCurveDef> upgradeCurves = new();
    [SerializeField] private List<PriceCurveDef> priceCurves = new();
    //런타임 전에 딕셔너리로 미리 구현 BuildCaches에서 실행
     private Dictionary<string, BlockDef> _blockById;
    
    private Dictionary<PickaxeTier, PickaxeTierPresetDef> _pickaxePresetByTier;
    private Dictionary<string, UpgradeCurveDef> _upgradeCurveById;
    private Dictionary<string, PriceCurveDef> _priceCurveById;

    private void OnEnalbe()
    {
        BuildCaches();
    }

     
    public BlockDef GetBlockDef(string blockId) // DB에서 ID로 블록 정보 받아오기
    {
        if (string.IsNullOrEmpty(blockId))
            return null;

        if (_blockById != null && _blockById.TryGetValue(blockId,out var def))
            return def;
        if (blockDefs != null)
        {
            for (int i = 0; i < blockDefs.Count; i++)
            {
                var d = blockDefs[i];
                if (d != null && d.blockId == blockId )
                {
                    _blockById?.Add(blockId,d);
                    return d;
                }
            }
        }
    #if UNITY_EDITOR
    
        Debug.LogError($"BlockDef not found: {blockId}");
    
    #endif
     return null;
    }

    /// <summary>
    /// 모든 블록 정의 목록을 반환한다.
    /// 왜 필요한가?
    /// - 디버그/에디터 툴/UI(도감) 등에서 전체 목록이 필요할 수 있다.
    /// - 단, 외부에서 리스트를 수정하면 DB가 오염되므로 "읽기 전용"으로 제공하는 게 안전하다.
    /// </summary>
    public IReadOnlyList<BlockDef> GetAllBlockDefs() //모든 블록 정의 목록을 반환
    {
        if (blockDefs == null) // 빈칸으로 넘긴다
        {
            return System.Array.Empty<BlockDef>();
        }
        return blockDefs;
    }

    
    

    // ─────────────────────────────────────────────────────────────────────────────
    // Public API: Pickaxe Tiers
    // ─────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// 곡괭이 등급 프리셋을 tier로 조회한다.
    /// 왜 필요한가?
    /// - PickaxeFactory가 곡괭이를 생성할 때 "Wood"면 어떤 기본 스탯인지 DB에서 받아서 초기화한다.
    /// - Pickaxe 진화(wood→stone) 시에도 tier만 바꾸고 프리셋 재적용이 가능해진다.
    /// </summary>
    public PickaxeTierPresetDef GetPickaxeTierPreset(PickaxeTier tier)
    {
        if (_pickaxePresetByTier != null &&
            _pickaxePresetByTier.TryGetValue(tier,out var presetDef))
            return presetDef;
    #if UNITY_EDITOR
        Debug.LogError ($"[GameDatabase] PickaxeTierPresetDef not found. tier={tier}");
    #endif
        return null;
    }

    public IReadOnlyList<PickaxeTierPresetDef> GetAllPickaxeTierPresets()
    {
        if (pickaxeTierPresets == null)
        {
            return System.Array.Empty<PickaxeTierPresetDef>();
        }
        return pickaxeTierPresets;
    }

    // ─────────────────────────────────────────────────────────────────────────────
    // Public API: Curves (Upgrade / Price)
    // ─────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// 업그레이드 효율 곡선(레벨→성능)을 ID로 조회한다.
    /// 왜 필요한가?
    /// - "레벨업 할수록 속도가 얼마나 늘지" 같은 밸런싱을 코드에서 하드코딩하지 않기 위해.
    /// - 밸런싱 수정이 잦은 게임에서 필수적인 데이터화 포인트.
    /// </summary>
    public UpgradeCurveDef GetUpgradeCurve(string curveId)
    {
        // TODO:
        // 1) null/empty 방어
        // 2) 캐시 조회
        // 3) 없으면 경고/에러
        return null;
    }

    /// <summary>
    /// 가격 곡선(레벨→비용)을 ID로 조회한다.
    /// 왜 필요한가?
    /// - ShopService는 "UpgradeCost" 같은 키로 가격 규칙을 받아와서 가격을 계산한다.
    /// - 가격 곡선만 바꿔 끼우면 경제 밸런싱을 빠르게 수정 가능.
    /// </summary>
    public PriceCurveDef GetPriceCurve(string curveId)
    {
        // TODO:
        // 1) null/empty 방어
        // 2) 캐시 조회
        // 3) 없으면 경고/에러
        return null;
    }

    public IReadOnlyList<UpgradeCurveDef> GetAllUpgradeCurves()
    {
        // TODO: IReadOnlyList로 반환
        return upgradeCurves;
    }

    public IReadOnlyList<PriceCurveDef> GetAllPriceCurves()
    {
        // TODO: IReadOnlyList로 반환
        return priceCurves;
    }

    // ─────────────────────────────────────────────────────────────────────────────
    // Cache Builder (TODO skeleton)
    // ─────────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// 리스트 데이터를 빠른 조회용 Dictionary로 변환한다.
    /// 왜 필요한가?
    /// - 성능: O(n) 탐색 → O(1) 조회
    /// - 안정성: ID 중복/누락을 개발 단계에서 즉시 감지
    /// </summary>
    private void BuildCaches()
    {
        // TODO:
        // - _blockById = new Dictionary<string, BlockDef>(capacity)
        // - foreach (var def in blockDefs) { 중복 체크 후 추가 }
        //
        // - _dropTableById ...
        // - _pickaxePresetByTier ...
        // - _upgradeCurveById ...
        // - _priceCurveById ...
    }

    // ─────────────────────────────────────────────────────────────────────────────
    // Validation (Optional)
    // ─────────────────────────────────────────────────────────────────────────────

#if UNITY_EDITOR
    private void OnValidate()
    {
        // TODO(선택):
        // - 에디터에서 값 바뀔 때마다 ID 중복/누락 검사
        // - 잘못된 데이터는 LogError로 바로 알려줘서, 런타임에서 터지는 걸 방지한다.
    }
#endif
}


}
