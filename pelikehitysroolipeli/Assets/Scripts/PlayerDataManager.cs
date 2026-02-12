using TMPro;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private int health;
    private int experience;
    private int coins;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI experienceText;
    private TextMeshProUGUI coinsText;



    void AddHealth(int amount)
    {
        if (amount > 0)
        {
            health += amount;

        }

    }
}
