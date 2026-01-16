using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        // 여기서 업그레이드 흐름을 시작
        Debug.Log("Pick 업그레이드 요청 수신");

        // 예:
        // pickManager.TryUpgrade();
        // economy.TrySpend();
    }
}
