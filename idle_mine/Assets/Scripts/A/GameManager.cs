using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Defs (READ)")]
    [SerializeField] private PriceCurveDef priceCurve;   // 에셋을 인스펙터로 꽂음

    [Header("Runtime")]
    [SerializeField] private EconomyWallet wallet;
    [SerializeField] private PickManager pickManager;

    private PickaxeProgress progress; // 세이브에서 로드된 값

    private void Awake()
    {
        progress = SaveSystem.LoadPickaxeProgress(); // tier 없이 level만 있다고 가정
        if (progress == null)
            progress = new PickaxeProgress { pickaxeId = "Pick_01", level = 1};
    }
    
    private void OnEnable()
    {
        // 이벤트 구독
        GameSignals.PickUpgradeRequested += OnPickUpgradeRequested;
    }

    private void OnDisable()
    {
        // 이벤트 해제 (필수)
        GameSignals.PickUpgradeRequested -= OnPickUpgradeRequested;
    }



    private void OnPickUpgradeRequested()
    {
        // (READ) progress.level
        int level = progress.level;

        // (READ) priceCurve로 cost 계산
        int cost = priceCurve.GetCost(level);

        // (USE) 지불 시도 (wallet 내부에서 money 읽음)
        if (!wallet.TrySpend(cost))
            return;

        // (USE) 성공하면 progress를 "수정하는" 주체는 PickManager
        pickManager.TryLevelUp(progress);

        // (선택) 저장
        SaveSystem.SavePickaxeProgress(progress);

    }
}
