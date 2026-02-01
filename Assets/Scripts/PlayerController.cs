using UnityEngine;
using UnityEngine.SceneManagement; // Added for scene switching

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Art Assets (Masks)")]
    public Sprite art1; // Red
    public Sprite art2; // Blue
    public Sprite art3; // Yellow
    public Sprite art4; // White

    [Header("Game Logic Data")]
    public string currentLocation = "Entry"; 
    public string equippedMask = "Red"; 
    
    [Header("Counters")]
    public int interactionCount = 0; // The counter you wanted
    public int maxInteractions = 8;

    private Rigidbody2D rb;
    private SpriteRenderer myRenderer;
    private Vector2 moveInput;

    void Awake()
    {
        // Clears the score list every time a new game starts
        NPCBase.finalScores.Clear();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();

        if(art1 != null) 
        {
            myRenderer.sprite = art1;
            equippedMask = "Red"; 
        }
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.x > 0) myRenderer.flipX = false;
        else if (moveInput.x < 0) myRenderer.flipX = true;

        // Mask Switching
        if (Input.GetKeyDown(KeyCode.Alpha1)) { myRenderer.sprite = art1; equippedMask = "Red"; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { myRenderer.sprite = art2; equippedMask = "Blue"; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { myRenderer.sprite = art3; equippedMask = "Yellow"; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { myRenderer.sprite = art4; equippedMask = "White"; }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    // NEW: Function to call when bumping into NPCs
    public void RegisterInteraction()
    {
        interactionCount++;
        Debug.Log("Interactions: " + interactionCount + "/8");

        if (interactionCount >= maxInteractions)
        {
            Debug.Log("Reached 8 interactions! Moving to EndGame.");
            NPCBase.SaveAllScores(); // SAVE HERE
            SceneManager.LoadScene("EndGame");
        }
    }
}