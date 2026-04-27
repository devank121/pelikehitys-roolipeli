using UnityEngine;
using UnityEngine.Rendering;

public class DoorController : MonoBehaviour
{
    public enum DoorState { Open, Closed, Locked }
    DoorState currentState;

    [SerializeField] Sprite ClosedDoorSprite;
    [SerializeField] Sprite OpenDoorSprite;
    [SerializeField] Sprite LockedSprite;
    [SerializeField] Sprite UnlockedSprite;

    BoxCollider2D colliderComp;

    public static Color lockedColor;
    public static Color openColor;

    SpriteRenderer doorSprite;
    SpriteRenderer lockSprite;

    [SerializeField] bool ShowDebugUI;
    [SerializeField] int DebugFontSize = 32;

    void Start()
    {
        doorSprite = GetComponent<SpriteRenderer>();
        colliderComp = GetComponent<BoxCollider2D>();
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        if (sprites.Length == 2 && sprites[0] == doorSprite)
            lockSprite = sprites[1];

        lockedColor = new Color(1.0f, 0.63f, 0.23f);
        openColor = new Color(0.5f, 0.8f, 1.0f);

        currentState = DoorState.Closed;
        CloseDoor();
        UnlockDoor();
    }

    public enum DoorAction { Open, Close, Lock, Unlock }

    public void ReceiveAction(DoorAction action)
    {
        switch (action)
        {
            case DoorAction.Open:
                if (currentState == DoorState.Closed)
                {
                    OpenDoor();
                    currentState = DoorState.Open;
                    AudioManager.Instance.PlaySound(SoundEffect.PlayerOpenDoor);
                }
                else
                {
                    AudioManager.Instance.PlaySound(SoundEffect.PlayerActionFailed);
                }
                break;

            case DoorAction.Close:
                if (currentState == DoorState.Open)
                {
                    CloseDoor();
                    currentState = DoorState.Closed;
                }
                else
                {
                    AudioManager.Instance.PlaySound(SoundEffect.PlayerActionFailed);
                }
                break;

            case DoorAction.Lock:
                if (currentState == DoorState.Closed)
                {
                    LockDoor();
                    currentState = DoorState.Locked;
                }
                else
                {
                    AudioManager.Instance.PlaySound(SoundEffect.PlayerActionFailed);
                }
                break;

            case DoorAction.Unlock:
                if (currentState == DoorState.Locked)
                {
                    UnlockDoor();
                    currentState = DoorState.Closed;
                }
                else
                {
                    AudioManager.Instance.PlaySound(SoundEffect.PlayerActionFailed);
                }
                break;
        }
    }

    private void OpenDoor()
    {
        doorSprite.sprite = OpenDoorSprite;
        colliderComp.isTrigger = true;
    }

    private void CloseDoor()
    {
        doorSprite.sprite = ClosedDoorSprite;
        colliderComp.isTrigger = false;
    }

    private void LockDoor()
    {
        lockSprite.sprite = LockedSprite;
        lockSprite.color = lockedColor;
    }

    private void UnlockDoor()
    {
        lockSprite.sprite = UnlockedSprite;
        lockSprite.color = openColor;
    }

    private void OnGUI()
    {
        if (ShowDebugUI == false) return;

        GUIStyle buttonStyle = GUI.skin.GetStyle("button");
        GUIStyle labelStyle = GUI.skin.GetStyle("label");
        buttonStyle.fontSize = DebugFontSize;
        labelStyle.fontSize = DebugFontSize;
        Rect guiRect = GetGuiRect();
        GUILayout.BeginArea(guiRect);
        GUILayout.Label("Door");
        if (GUILayout.Button("Open")) OpenDoor();
        if (GUILayout.Button("Close")) CloseDoor();
        if (GUILayout.Button("Lock")) LockDoor();
        if (GUILayout.Button("Unlock")) UnlockDoor();
        GUILayout.EndArea();
    }

    private Rect GetGuiRect()
    {
        Vector3 buttonPos = transform.position;
        buttonPos.x += 1;
        buttonPos.y -= 0.25f;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(buttonPos);
        float screenHeight = Screen.height;
        return new Rect(screenPoint.x, screenHeight - screenPoint.y,
            DebugFontSize * 8, DebugFontSize * 100);
    }
}