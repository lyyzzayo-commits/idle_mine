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

    [Header("Drop Tables")]
    [SerializeField] private List<DropTableDef> dropTableDefs = new();

    [Header("Pickaxe Tiers")]
    [SerializeField] private List<PickaxeTierPresetDef> pickaxeTierPresets = new();

    [Header("Curves")]
    [SerializeField] private List<UpgradeCurveDef> upgradeCurves = new();
    [SerializeField] private List<PriceCurveDef> priceCurves = new();
    //런타임 전에 딕셔너리로 미리 구현 BuildCaches에서 실행
     private Dictionary<string, BlockDef> _blockById;
    private Dictionary<string, DropTableDef> _dropTableById;
    private Dictionary<PickaxeTier, PickaxeTierPresetDef> _pickaxePresetByTier;
    private Dictionary<string, UpgradeCurveDef> _upgradeCurveById;
    private Dictionary<string, PriceCurveDef> _priceCurveById;

    private void OnEnalbe()
    {
        BuildCaches();
    }

    private void BuildCaches()
    {
        
    }


}
