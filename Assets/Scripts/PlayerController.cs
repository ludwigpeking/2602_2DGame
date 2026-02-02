using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Art Assets (Masks)")]
    public Sprite art1; // Red
    public Sprite art2; // Blue
    public Sprite art3; // Yellow
    public Sprite art4; // White

    [Header("UI Groups (Drag Objects Here)")]
    // The "Normal" (Off) states
    public GameObject group_Red;
    public GameObject group_Blue;
    public GameObject group_Yellow;
    public GameObject group_White;
    // The "Active" (On) states
    public GameObject group_RedOn;
    public GameObject group_BlueOn;
    public GameObject group_YellowOn;
    public GameObject group_WhiteOn;

    [Header("Game Logic Data")]
    public string currentLocation = "Entry"; 
    public string equippedMask = "Red"; 
    
    [Header("Counters")]
    public int interactionCount = 0;
    public int maxInteractions = 8;

    private Rigidbody2D rb;
    private SpriteRenderer myRenderer;
    private Vector2 moveInput;

    void Awake()
    {
        NPCBase.finalScores.Clear();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();

        // Set Default Mask (Red) and update UI
        if(art1 != null) 
        {
            myRenderer.sprite = art1;
            equippedMask = "Red";
            UpdateMaskUI("Red"); // <--- Update UI at start
        }
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.x > 0) myRenderer.flipX = false;
        else if (moveInput.x < 0) myRenderer.flipX = true;

        // Mask Switching with UI Updates
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        { 
            myRenderer.sprite = art1; 
            equippedMask = "Red"; 
            UpdateMaskUI("Red"); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        { 
            myRenderer.sprite = art2; 
            equippedMask = "Blue"; 
            UpdateMaskUI("Blue"); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) 
        { 
            myRenderer.sprite = art3; 
            equippedMask = "Yellow"; 
            UpdateMaskUI("Yellow"); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) 
        { 
            myRenderer.sprite = art4; 
            equippedMask = "White"; 
            UpdateMaskUI("White"); 
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    public void RegisterInteraction()
    {
        interactionCount++;
        Debug.Log("Interactions: " + interactionCount + "/8");

        if (interactionCount >= maxInteractions)
        {
            Debug.Log("Reached 8 interactions! Moving to EndGame.");
            NPCBase.SaveAllScores(); 
            SceneManager.LoadScene("EndGame");
        }
    }

    // --- NEW HELPER FUNCTION ---
    void UpdateMaskUI(string activeColor)
    {
        // 1. Reset everything to "Off" state first
        // Turn ON all standard groups
        if(group_Red) group_Red.SetActive(true);
        if(group_Blue) group_Blue.SetActive(true);
        if(group_Yellow) group_Yellow.SetActive(true);
        if(group_White) group_White.SetActive(true);

        // Turn OFF all "On" groups
        if(group_RedOn) group_RedOn.SetActive(false);
        if(group_BlueOn) group_BlueOn.SetActive(false);
        if(group_YellowOn) group_YellowOn.SetActive(false);
        if(group_WhiteOn) group_WhiteOn.SetActive(false);

        // 2. Turn on the specific "On" group for the active color
        //    and hide its "Off" group
        switch (activeColor)
        {
            case "Red":
                if(group_RedOn) group_RedOn.SetActive(true);
                if(group_Red) group_Red.SetActive(false);
                break;
            case "Blue":
                if(group_BlueOn) group_BlueOn.SetActive(true);
                if(group_Blue) group_Blue.SetActive(false);
                break;
            case "Yellow":
                if(group_YellowOn) group_YellowOn.SetActive(true);
                if(group_Yellow) group_Yellow.SetActive(false);
                break;
            case "White":
                if(group_WhiteOn) group_WhiteOn.SetActive(true);
                if(group_White) group_White.SetActive(false);
                break;
        }
    }
}