using UnityEngine;

public enum PriceCurveType
{
    Linear,     // 선형 증가
    Exponential,// 지수 증가
    Curve       // AnimationCurve 사용
}

[CreateAssetMenu(
    fileName = "PriceCurve_",
    menuName = "Game/Economy/PriceCurveDef"
)]
public class PriceCurveDef : ScriptableObject
{
    [Header("Identity")]
    public string curveId;              // "UpgradeCost", "BuyPickaxeCost" 등
    public PriceCurveType curveType;

    [Header("Base")]
    public int baseCost = 10;           // level 1 기준 비용

    [Header("Linear")]
    public int linearIncrease = 5;      // level당 증가량

    [Header("Exponential")]
    public float exponentialRate = 1.15f; // level^rate 계수

    [Header("Curve")]
    public AnimationCurve customCurve;  // x=level, y=multiplier

    [Header("Clamp")]
    public int minCost = 0;
    public int maxCost = 0;             // 0이면 무제한
}
