using UnityEngine;

public class CrowdWiggle : MonoBehaviour
{
    [Header("Settings")]
    public float moveChance = 0.002f; // 1% chance to move
    public float pixelStep = 0.0125f; // Distance to move (1 pixel)

    private SpriteRenderer sr;
    private Rigidbody2D rb;

 void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // --- NEW CODE STARTS HERE ---
        
        // Flip a coin: 0 or 1
        int randomDirection = Random.Range(0, 2);

        if (randomDirection == 0)
        {
            sr.flipX = true;  // Face Left
        }
        else
        {
            sr.flipX = false; // Face Right
        }
        
        // --- NEW CODE ENDS HERE ---
    }

    void FixedUpdate()
    {
        // 1. Roll the dice (1% chance every physics frame)
        if (Random.value < moveChance)
        {
            PerformRandomMove();
        }
    }

    void PerformRandomMove()
    {
        // 2. Pick random direction: -1 (Left) or 1 (Right)
        float direction = (Random.Range(0, 2) == 0) ? -1f : 1f;

        // 3. Calculate new position
        Vector2 targetPosition = rb.position + new Vector2(direction * pixelStep, 0);

        // 4. Move smoothly using Physics
        rb.MovePosition(targetPosition);

        // 5. Flip the image to look where it is going
        if (direction > 0)
        {
            sr.flipX = false; // Facing Right
        }
        else
        {
            sr.flipX = true;  // Facing Left
        }
    }
}