using UnityEngine;

public class RuokaAnnos : Tavara
{
    public override bool Use(PlayerController player)
    {
        PlayerDataManager.Instance.AddHealth(10);
        return true;
    }
}