using UnityEngine;

public class Vesi : Tavara
{
    public override bool Use(PlayerController player)
    {
        PlayerDataManager.Instance.AddHealth(20);
        return true;
    }
}