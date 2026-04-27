using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerTrading : MonoBehaviour
{
    public GameObject merchantPanel;
    public TextMeshProUGUI nameLabel;
    public Button buyButton;
    public Button cancelButton;
    public TMP_Dropdown dropdown;
    private MerchantInfo currentMerchant;

    private void Start()
    {
        merchantPanel.SetActive(false);
        cancelButton.onClick.AddListener(CloseMerchantUI);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MerchantInfo merchant = collision.GetComponent<MerchantInfo>();
        if (merchant != null)
        {
            currentMerchant = merchant;
            AudioManager.Instance.PlaySound(SoundEffect.PlayerFoundMerchant);
            OpenMerchantUI();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MerchantInfo merchant = collision.GetComponent<MerchantInfo>();
        if (merchant != null && merchant == currentMerchant)
            CloseMerchantUI();
    }

    void OpenMerchantUI()
    {
        merchantPanel.SetActive(true);
        if (currentMerchant.merchantType == MerchantType.ArrowMerchant)
            SetupArrowMerchant();
        else if (currentMerchant.merchantType == MerchantType.FoodMerchant)
            SetupFoodMerchant();
    }

    void CloseMerchantUI()
    {
        merchantPanel.SetActive(false);
        currentMerchant = null;
    }

    void SetupArrowMerchant()
    {
        nameLabel.text = "Arrow Merchant";
        dropdown.ClearOptions();
        string[] arrowTypes = { "Wood Arrow (10)", "Fire Arrow (20)", "Ice Arrow (30)" };
        dropdown.AddOptions(new System.Collections.Generic.List<string>(arrowTypes));
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(BuyArrow);
    }

    void BuyArrow()
    {
        int selected = dropdown.value;
        int cost = selected == 0 ? 10 : selected == 1 ? 20 : 30;

        if (PlayerDataManager.Instance.TakeMoney(cost))
        {
            AudioManager.Instance.PlaySound(SoundEffect.PlayerPurchase);
            Debug.Log("Arrow purchased!");
        }
        else
        {
            AudioManager.Instance.PlaySound(SoundEffect.PlayerActionFailed);
            Debug.Log("Not enough money!");
        }
    }

    void SetupFoodMerchant()
    {
        nameLabel.text = "Food Merchant";
        dropdown.ClearOptions();
        string[] foodTypes = { "Small Meal (15)", "Medium Meal (25)", "Big Meal (40)" };
        dropdown.AddOptions(new System.Collections.Generic.List<string>(foodTypes));
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(BuyFood);
    }

    void BuyFood()
    {
        int selected = dropdown.value;
        int cost = selected == 0 ? 15 : selected == 1 ? 25 : 40;
        int hpGain = selected == 0 ? 10 : selected == 1 ? 20 : 35;

        if (PlayerDataManager.Instance.TakeMoney(cost))
        {
            PlayerDataManager.Instance.AddHealth(hpGain);
            AudioManager.Instance.PlaySound(SoundEffect.PlayerPurchase);
            Debug.Log("Food purchased!");
        }
        else
        {
            AudioManager.Instance.PlaySound(SoundEffect.PlayerActionFailed);
            Debug.Log("Not enough money!");
        }
    }
}