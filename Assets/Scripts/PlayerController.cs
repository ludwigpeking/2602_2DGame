using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Art Assets (Masks)")]
    public Sprite art1; // Red (Default)
    public Sprite art2; // Blue
    public Sprite art3; // Yellow
    public Sprite art4; // White

    [Header("Game Logic Data")]
    public string currentLocation = "Entry"; 
    // Default is now "red" to match your art
    public string equippedMask = "red"; 

    private Rigidbody2D rb;
    private SpriteRenderer myRenderer;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();

        // Initialize with the Red mask (art1)
        if(art1 != null) 
        {
            myRenderer.sprite = art1;
            equippedMask = "red"; 
        }
    }

    void Update()
    {
        // 1. Movement Input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // 2. Flip Sprite (Face Left/Right)
        if (moveInput.x > 0) myRenderer.flipX = false;
        else if (moveInput.x < 0) myRenderer.flipX = true;

        // 3. Switch Masks & Update Data
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        { 
            myRenderer.sprite = art1; 
            equippedMask = "red"; 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        { 
            myRenderer.sprite = art2; 
            equippedMask = "blue"; 
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) 
        { 
            myRenderer.sprite = art3; 
            equippedMask = "yellow"; 
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) 
        { 
            myRenderer.sprite = art4; 
            equippedMask = "white"; 
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}