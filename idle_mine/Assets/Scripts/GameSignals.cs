using System;

public static class GameSignals
{
    public static event Action PickDrag;
    public static void RaisePickDrag() => PickDrag?.Invoke();

    public static event Action PickDragEnd;
    public static void RaisePickDragEnd() => PickDragEnd?.Invoke();

    public static event Action PickUpgradeRequested;
    public static void RaisePickUpgradeRequested() => PickUpgradeRequested?.Invoke();
}
