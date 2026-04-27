using UnityEngine;

public class Nuoli : Tavara
{
    public GameObject projectilePrefab;

    public override bool Use(PlayerController player)
    {
        player.SetArrowProjectilePrefab(projectilePrefab);
        return true;
    }
}