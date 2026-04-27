using UnityEngine;

public class Jousi : Tavara
{
    public override bool Use(PlayerController player)
    {
        player.SetChosenWeapon(gameObject);
        return true;
    }
}