using UnityEngine;

public class Tavara : MonoBehaviour
{
    [SerializeField] private float paino;
    [SerializeField] private float tilavuus;

    public float Paino { get { return paino; } }
    public float Tilavuus { get { return tilavuus; } }

    public Tavara TeeKopio()
    {
        return (Tavara)MemberwiseClone();
    }

    public override string ToString()
    {
        return GetType().Name;
    }

    // Base Use method - can be overridden by each item
    public virtual bool Use(PlayerController player)
    {
        return false;
    }
}