using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class EconomyWallet : MonoBehaviour
{
    private int Mymoney = 0;
    public bool TrySpend(int cost)
    {
        if (cost > Mymoney) return false;

        Mymoney -= cost;

        return true;
    }
}
