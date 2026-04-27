using UnityEngine;

public class Miekka : Tavara
{
    public override bool Use(PlayerController player)
    {
        player.SetChosenWeapon(gameObject);
        return true;
    }
}