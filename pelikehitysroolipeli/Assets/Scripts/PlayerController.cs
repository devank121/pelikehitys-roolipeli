using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Vector2 lastMovement;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject buttonPanel;
    [SerializeField] TMP_Text reppuText;
    [SerializeField] ReppuUI reppuUI;
    DoorController currentDoor;
    Button openButton;
    Button closeButton;
    Button lockButton;
    Button unlockButton;
    Reppu reppu = new Reppu(10, 10.0f, 15.0f);

    // New variables for chosen weapon and arrow
    GameObject chosenArrowPrefab;
    GameObject chosenWeaponPrefab;

    void Start()
    {
        lastMovement = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        openButton = GameObject.Find("OpenButton").GetComponent<Button>();
        closeButton = GameObject.Find("CloseButton").GetComponent<Button>();
        lockButton = GameObject.Find("LockButton").GetComponent<Button>();
        unlockButton = GameObject.Find("UnlockButton").GetComponent<Button>();
        openButton.onClick.AddListener(OnOpenButton);
        closeButton.onClick.AddListener(OnCloseButton);
        lockButton.onClick.AddListener(OnLockButton);
        unlockButton.onClick.AddListener(OnUnlockButton);
        buttonPanel.SetActive(false);

        // Show empty backpack text when game starts
        P‰ivit‰ReppuUI();
    }

    void Update() { }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + lastMovement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            AudioManager.Instance.PlaySound(SoundEffect.PlayerHitWall);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            currentDoor = collision.GetComponent<DoorController>();
            buttonPanel.SetActive(true);
        }
        else if (collision.CompareTag("Merchant"))
        {
            AudioManager.Instance.PlaySound(SoundEffect.PlayerFoundMerchant);
        }
        else if (collision.CompareTag("Item"))
        {
            Tavara tavaraKomponentti = collision.GetComponent<Tavara>();
            if (tavaraKomponentti != null)
            {
                Tavara tavaraKopio = tavaraKomponentti.TeeKopio(); // make a copy
                bool onnistui = reppu.Lis‰‰(tavaraKopio);          // try to add to backpack

                if (onnistui)
                {
                    Debug.Log("Lis‰tty reppuun: " + tavaraKopio.ToString());
                    Destroy(collision.gameObject);                  // remove from scene
                    P‰ivit‰ReppuUI();                              // update the UI
                }
                else
                {
                    Debug.Log("Ei mahdu reppuun!");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            currentDoor = null;
            buttonPanel.SetActive(false);
        }
    }

    void OnMoveAction(InputValue value)
    {
        lastMovement = value.Get<Vector2>();
    }

    void OnOpenButton()
    {
        if (currentDoor != null)
        {
            currentDoor.ReceiveAction(DoorController.DoorAction.Open);
            AudioManager.Instance.PlaySound(SoundEffect.PlayerOpenDoor);
        }
        else
        {
            AudioManager.Instance.PlaySound(SoundEffect.PlayerActionFailed);
        }
    }

    void OnCloseButton()
    {
        if (currentDoor != null)
            currentDoor.ReceiveAction(DoorController.DoorAction.Close);
    }

    void OnLockButton()
    {
        if (currentDoor != null)
            currentDoor.ReceiveAction(DoorController.DoorAction.Lock);
    }

    void OnUnlockButton()
    {
        if (currentDoor != null)
            currentDoor.ReceiveAction(DoorController.DoorAction.Unlock);
    }

    // Set the chosen arrow prefab
    public void SetArrowProjectilePrefab(GameObject projectile)
    {
        chosenArrowPrefab = projectile;
        Debug.Log("Valittu nuoli: " + projectile.name);
    }

    // Set the chosen weapon
    public void SetChosenWeapon(GameObject weapon)
    {
        chosenWeaponPrefab = weapon;
        Debug.Log("Valittu ase: " + weapon.name);
    }

    // Use an item from the backpack by index
    public void K‰yt‰Esine(int index)
    {
        Tavara tavara = reppu.HaeTavara(index);
        if (tavara != null)
        {
            bool onnistui = tavara.Use(this);
            if (onnistui)
            {
                Debug.Log("K‰ytetty: " + tavara.ToString());
            }
        }
    }

    void P‰ivit‰ReppuUI()
    {
        reppuText.text = reppu.ToString();
        reppuUI.P‰ivit‰ReppuUI(reppu);
    }
}