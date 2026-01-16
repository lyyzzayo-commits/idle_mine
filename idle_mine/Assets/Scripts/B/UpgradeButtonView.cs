using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonView : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;

    private void Awake()
    {
        upgradeButton.onClick.AddListener(OnClickUpgrade);
    }

    private void OnClickUpgrade()
    {
        GameSignals.RaisePickUpgradeRequested();
    }
}
