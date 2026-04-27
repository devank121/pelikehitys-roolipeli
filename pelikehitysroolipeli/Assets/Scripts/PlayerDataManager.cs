using TMPro;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    // ? Singleton instance
    public static PlayerDataManager Instance;

    // Player values
    private int health = 100;
    private int experience = 0;
    private int coins = 0;

    // UI references
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private TextMeshProUGUI coinsText;
    // Debug UI
    [SerializeField] private bool ShowDebugUI = true;
    [SerializeField] private int DebugFontSize = 24;

    void Awake()
    {
        // ? Singleton logic
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

   

    public void AddExperience(int amount)
    {
        if (amount > 0)
        {
            experience += amount;
            UpdateUI();
        }
    }

    public void AddHealth(int amount)
    {
        if (amount > 0)
        {
            health += amount;
            UpdateUI();
        }
    }

    public int RemoveHealth(int damageAmount)
    {
        if (damageAmount > 0)
        {
            health -= damageAmount;

            if (health < 0)
                health = 0;

            UpdateUI();
        }

        return health;
    }

    public int AddMoney(int coinAmount)
    {
        if (coinAmount > 0)
        {
            coins += coinAmount;
            UpdateUI();
        }

        return coins;
    }

    public bool TakeMoney(int coinAmount)
    {
        if (coinAmount > 0 && coins >= coinAmount)
        {
            coins -= coinAmount;
            UpdateUI();
            return true;
        }

        return false;
    }

    

    private void UpdateUI()
    {
        if (healthText != null)
            healthText.text = "HP: " + health;

        if (experienceText != null)
            experienceText.text = "XP: " + experience;

        if (coinsText != null)
            coinsText.text = "Coins: " + coins;
    }

   

    private void OnGUI()
    {
        if (!ShowDebugUI) return;

        GUIStyle buttonStyle = GUI.skin.GetStyle("button");
        GUIStyle labelStyle = GUI.skin.GetStyle("label");

        buttonStyle.fontSize = DebugFontSize;
        labelStyle.fontSize = DebugFontSize;

        GUILayout.BeginArea(new Rect(20, 20, 300, 500));

        GUILayout.Label("Player Data");

        if (GUILayout.Button("Add 10 XP"))
            AddExperience(10);

        if (GUILayout.Button("Add 10 Health"))
            AddHealth(10);

        if (GUILayout.Button("Take 10 Damage"))
            RemoveHealth(10);

        if (GUILayout.Button("Add 5 Coins"))
            AddMoney(5);

        if (GUILayout.Button("Spend 5 Coins"))
            TakeMoney(5);

        GUILayout.EndArea();
    }
}
