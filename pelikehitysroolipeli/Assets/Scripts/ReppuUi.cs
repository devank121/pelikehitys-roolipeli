using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReppuUI : MonoBehaviour
{
    [SerializeField] GameObject tavaraButtonPrefab;  // the button template
    [SerializeField] Transform content;              // the Content object inside Scroll View
    PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Call this every time the backpack changes
    public void P‰ivit‰ReppuUI(Reppu reppu)
    {
        // Hide panel if backpack is empty, show if it has items
        gameObject.SetActive(reppu.TavaroidenMaara > 0);

       
        // First clear all existing buttons
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // Create a button for each item
        for (int i = 0; i < reppu.TavaroidenMaara; i++)
        {
            Tavara tavara = reppu.HaeTavara(i);

            // Create a new button from the prefab
            GameObject uusiNappi = Instantiate(tavaraButtonPrefab, content);

            // Set the button text to the item name
            TMP_Text nappiTeksti = uusiNappi.GetComponentInChildren<TMP_Text>();
            nappiTeksti.text = tavara.ToString();

            // When clicked, use that item
            int index = i; // we need to save i here, otherwise all buttons use the last value
            Button nappi = uusiNappi.GetComponent<Button>();
            nappi.onClick.AddListener(() => playerController.K‰yt‰Esine(index));
        }
    }
}