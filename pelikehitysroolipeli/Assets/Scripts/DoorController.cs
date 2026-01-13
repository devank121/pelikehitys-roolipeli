using UnityEngine;
using UnityEngine.Rendering;

public class DoorController : MonoBehaviour
{
    // Kuvat oven eri tiloille
    [SerializeField]
    Sprite ClosedDoorSprite;
    [SerializeField]
    Sprite OpenDoorSprite;
    [SerializeField]
    Sprite LockedSprite;
    [SerializeField]
    Sprite UnlockedSprite;

    [SerializeField]
    bool ShowDebugUI;

    BoxCollider2D colliderComp;

    // Näitä värejä käytetään lukkosymbolin piirtämiseen.
    public static Color lockedColor;
    public static Color openColor;

    SpriteRenderer doorSprite; // Oven kuva
    SpriteRenderer lockSprite; // Lapsi gameobjectissa oleva lukon kuva

    void Start()
    {
        doorSprite = GetComponent<SpriteRenderer>();
        colliderComp = GetComponent<BoxCollider2D>();
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        if (sprites.Length == 2 && sprites[0] == doorSprite)
        {
            lockSprite = sprites[1];
        }

        
        lockedColor = new Color(1.0f, 0.63f, 0.23f);
        openColor = new Color(0.5f, 0.8f, 1.0f);


         // TODO
         // missä tilassa ovi on kun peli alkaa?
    }

    /// <summary>
    /// Oveen kohdistuu jokin toiminto joka muuttaa sen tilaa
    /// </summary>
    public void ReceiveAction()
    {
        
    }

    // Kun tulee toiminto, sen perusteella kutsutaan jotakin
    // näistä funktioista että oven tila muuttuu

    /// <summary>
    /// Vaihtaa oven kuvan avoimeksi oveksi
    /// ja laittaa törmäysalueen pois päältä
    /// </summary>
    private void OpenDoor()
    {
        doorSprite.sprite = OpenDoorSprite;
        colliderComp.isTrigger = true;
    }

    /// <summary>
    /// Vaihtaa oven kuvan suljetuksi oveksi
    /// ja laittaa törmäysalueen päälle
    /// </summary>
    private void CloseDoor()
    {
        doorSprite.sprite = ClosedDoorSprite;
        colliderComp.isTrigger = false;
    }

    /// <summary>
    /// Vaihtaa lukkosymbolin lukituksi ja
    /// vaihtaa sen värin
    /// </summary>
    private void LockDoor()
    {
        lockSprite.sprite = LockedSprite;
        lockSprite.color = lockedColor;
    }

    /// <summary>
    /// Vaihtaa lukkosymbolin avatuksi ja
    /// vaihtaa sen värin
    /// </summary>
    private void UnlockDoor()
    {
        lockSprite.sprite = UnlockedSprite;
        lockSprite.color = openColor;
    }

    // *********************************
    // Unityssä on välittömän käyttöliittymän
    // järjestelmä, jolla voi piirtää 
    // nappeja ja tekstiä koodin avulla.
    // Se on kätevä erilaisten oikoteiden ja
    // testaamisen apuvälineiden kehittämiseen.
    // Tässä sitä on käytetty tekemään napit, joilla
    // voi testata että oven eri toiminnot
    // toimivat oikein.


    Rect guiRect; //< Seuraavan napin alue

    // Unity kutsuu tätä funktiota kaiken muun piirtämisen jälkeen
    // Sen sisällä voi piirtää käyttöliittymää
    private void OnGUI()
    {
        if (ShowDebugUI == false)
        {
            return;
        }
        ResetGuiRect();
        if (GUI.Button(NextGuiRect(), "Open"))
        {
            OpenDoor();
        }
        if (GUI.Button(NextGuiRect(), "Close"))
        {
            CloseDoor();
        }
        if (GUI.Button(NextGuiRect(), "Lock"))
        {
            LockDoor();
        }
        if (GUI.Button(NextGuiRect(), "Unlock"))
        {
            UnlockDoor();
        }
    }

    // Näiden kahden funktion avulla ei tarvitse itse
    // määrittää jokaisen napin paikkaa, vaan ne
    // ladotaan automaattisesti allekkain.
    private Rect NextGuiRect()
    {
        Rect next = guiRect;
        guiRect.y += 32;
        return next;
    }

    private void ResetGuiRect()
    {
        Vector3 buttonPos = transform.position;
        buttonPos.x += 1;
        buttonPos.y -= 0.25f;
        // Tällä tavalla voi laskea paikan jossa GameObject näkyy
        // ruudulla ja piirtää käyttöliittymän sen kohdalle.
        // WorldToScreenPoint antaa Y koordinaatin väärin päin
        // tai GUI koodi ymmärtää sen väärin,
        // ja siksi se pitä vähentää ruudun korkeudesta.
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(buttonPos);
        float screenHeight = Screen.height;
        guiRect = new Rect(screenPoint.x, screenHeight - screenPoint.y, 100, 32);
    }    
}
