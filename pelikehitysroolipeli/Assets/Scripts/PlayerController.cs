using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector2 lastMovement;
    Rigidbody2D rb;

    [SerializeField] float moveSpeed;

    // Reference to button parent (assign in Inspector)
    [SerializeField] GameObject buttonPanel;

    // Door reference
    DoorController currentDoor;

    // Button references
    Button openButton;
    Button closeButton;
    Button lockButton;
    Button unlockButton;

    void Start()
    {
        lastMovement = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();

        // Find buttons
        openButton = GameObject.Find("OpenButton").GetComponent<Button>();
        closeButton = GameObject.Find("CloseButton").GetComponent<Button>();
        lockButton = GameObject.Find("LockButton").GetComponent<Button>();
        unlockButton = GameObject.Find("UnlockButton").GetComponent<Button>();

        // Connect buttons
        openButton.onClick.AddListener(OnOpenButton);
        closeButton.onClick.AddListener(OnCloseButton);
        lockButton.onClick.AddListener(OnLockButton);
        unlockButton.onClick.AddListener(OnUnlockButton);

        // Hide buttons at start
        buttonPanel.SetActive(false);
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + lastMovement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            Debug.Log("Found Door");

            // Store DoorController reference
            currentDoor = collision.GetComponent<DoorController>();

            // Show buttons
            buttonPanel.SetActive(true);
        }
        else if (collision.CompareTag("Merchant"))
        {
            Debug.Log("Found Merchant");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            currentDoor = null;

            // Hide buttons
            buttonPanel.SetActive(false);
        }
    }

    void OnMoveAction(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        lastMovement = v;
    }

    // Button functions

    void OnOpenButton()
    {
        if (currentDoor != null)
            currentDoor.ReceiveAction(DoorController.DoorAction.Open);
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
}