using UnityEngine;

public class NPCYellow : NPCBase
{


    [Header("Mask Counters")]
    private int redCount = 0;
    private int blueCount = 0;
    private int yellowCount = 0;
    private int whiteCount = 0;

    [Header("Room Locations")]
    public Transform libraryLoc;
    public Transform loungeLoc;
    public Transform diningLoc;
    public Transform ballroomLoc;
    public Transform gardenLoc;

    protected override void OnPlayerInteract(PlayerController player)
    {
        string mask = player.equippedMask.ToUpper();

        if (mask == "RED") HandleRed();
        else if (mask == "BLUE") HandleBlue();
        else if (mask == "YELLOW") HandleYellow();
        else if (mask == "WHITE") HandleWhite();

        Debug.Log($"NPC_Yellow: Love={loveScore}, Hate={hateScore}");
    }

    void HandleRed()
    {
        redCount++;
        // Rows 1-3: +1 Hate. Row 4+: Specific Rooms. Row 8: +2 Love.
        if (redCount <= 3) hateScore += 1;
        else if (redCount == 4) TeleportTo(loungeLoc);
        else if (redCount == 5) TeleportTo(diningLoc);
        else if (redCount == 6) TeleportTo(ballroomLoc);
        else if (redCount == 7) TeleportTo(gardenLoc);
        else if (redCount >= 8) loveScore += 2;
    }

    void HandleBlue()
    {
        blueCount++;
        // Row 1: Go to Library. Row 2-8: +1 or +2 Love.
        if (blueCount == 1) TeleportTo(libraryLoc);
        else if (blueCount == 4 || blueCount == 6 || blueCount == 8) loveScore += 2;
        else loveScore += 1;
    }

    void HandleYellow()
    {
        yellowCount++;
        // Sequence: +2, +2, +2, +1, +1, +1, +2, +2 Love
        if (yellowCount <= 3 || yellowCount >= 7) loveScore += 2;
        else loveScore += 1;
    }

    void HandleWhite()
    {
        whiteCount++;
        // Sequence: +1, +1, +1, +2, +1, +1, +1, +2 Love
        if (whiteCount == 4 || whiteCount == 8) loveScore += 2;
        else loveScore += 1;
    }

    void TeleportTo(Transform target)
    {
        if (target != null) transform.position = target.position;
        else Debug.LogWarning("Target location not assigned in NPC_Yellow!");
    }
}