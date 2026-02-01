using UnityEngine;

public class NPCBlue : NPCBase
{


    [Header("Individual Mask Counters")]
    private int redCount = 0;
    private int blueCount = 0;
    private int yellowCount = 0;
    private int whiteCount = 0;

    [Header("Sequential Travel")]
    public Transform[] travelPoints;
    private int currentPointIndex = 0;

    protected override void OnPlayerInteract(PlayerController player)
    {
        string mask = player.equippedMask.ToUpper();

        // Branching logic based on which mask was used
        if (mask == "RED") HandleRedMask();
        else if (mask == "BLUE") HandleBlueMask();
        else if (mask == "YELLOW") HandleYellowMask();
        else if (mask == "WHITE") HandleWhiteMask();

        Debug.Log($"NPC_Blue Status: Love={loveScore}, Hate={hateScore} | Counts: R{redCount} B{blueCount} Y{yellowCount} W{whiteCount}");
    }

    // RED MASK LOGIC (Table Col 1)
    void HandleRedMask()
    {
        redCount++;
        // Rows 2, 4, 7, 8 give Love
        if (redCount == 2 || redCount == 4 || redCount == 7) { loveScore += 2; Travel(); }
        else if (redCount == 3 || redCount == 5 || redCount == 8) { loveScore += 1; Travel(); }
        else if (redCount == 1 || redCount == 6) { Travel(); } // Just travel
    }

    // BLUE MASK LOGIC (Table Col 2)
    void HandleBlueMask()
    {
        blueCount++;
        if (blueCount == 8) return; // "Nothing" for 8th Blue hit

        if (blueCount == 1 || blueCount == 6) loveScore += 2;
        else if (blueCount == 2 || blueCount == 4 || blueCount == 7) loveScore += 1;
        
        Travel(); // All Blue hits (except 8) trigger Travel
    }

    // YELLOW MASK LOGIC (Table Col 3)
    void HandleYellowMask()
    {
        yellowCount++;
        if (yellowCount == 3 || yellowCount == 5 || yellowCount == 8) loveScore += 2;
        else if (yellowCount == 1 || yellowCount == 6) loveScore += 1;

        // Note: Rows 2, 4, 7 are "Travel" only (no score increase)
        Travel(); 
    }

    // WHITE MASK LOGIC (Table Col 4)
    void HandleWhiteMask()
    {
        whiteCount++;
        hateScore += 1; // Every White hit adds +1 Hate
        Travel();       // Every White hit triggers Travel
    }

    void Travel()
    {
        if (travelPoints != null && travelPoints.Length > 0)
        {
            transform.position = travelPoints[currentPointIndex].position;
            currentPointIndex = (currentPointIndex + 1) % travelPoints.Length;
        }
    }
}