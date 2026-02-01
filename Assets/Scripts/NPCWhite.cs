using UnityEngine;

public class NPCWhite : NPCBase
{


    [Header("Mask Counters")]
    private int redCount = 0;
    private int blueCount = 0;
    private int yellowCount = 0;
    private int whiteCount = 0;

    [Header("Specific Tour Locations")]
    public Transform barLoc;
    public Transform diningLoc;
    public Transform loungeLoc;
    public Transform libraryLoc;
    public Transform gardenLoc;
    public Transform foyerLoc;

    private int travelStep = 0;

    protected override void OnPlayerInteract(PlayerController player)
    {
        string mask = player.equippedMask.ToUpper();

        // 1. Process Score Patterns from Table (image_269af5.png)
        if (mask == "RED") HandleRed();
        else if (mask == "BLUE") HandleBlue();
        else if (mask == "YELLOW") HandleYellow();
        else if (mask == "WHITE") HandleWhite();

        // 2. Process Sequential Travel
        AdvanceTour();

        Debug.Log($"NPC_White: Love={loveScore}, Hate={hateScore} | Step={travelStep}");
    }

    void HandleRed()
    {
        redCount++;
        if (redCount == 2 || redCount == 3 || redCount == 4 || redCount == 6) loveScore += (redCount == 3) ? 2 : 1;
        else hateScore += 1; // Rows 1, 5, 7, 8
    }

    void HandleBlue()
    {
        blueCount++;
        if (blueCount == 1 || blueCount == 3 || blueCount == 4 || blueCount == 5) loveScore += (blueCount == 4) ? 2 : 1;
        else hateScore += 1; // Rows 2, 6, 7, 8
    }

    void HandleYellow()
    {
        yellowCount++;
        if (yellowCount == 1 || yellowCount == 2 || yellowCount == 3 || yellowCount == 5 || yellowCount == 6) loveScore += (yellowCount == 2) ? 2 : 1;
        else hateScore += 1; // Rows 4, 7, 8
    }

    void HandleWhite()
    {
        whiteCount++;
        if (whiteCount == 1 || whiteCount == 2 || whiteCount == 4 || whiteCount == 5 || whiteCount == 6) loveScore += (whiteCount == 1 || whiteCount == 5 || whiteCount == 6) ? 2 : 1;
        else hateScore += 1; // Rows 3, 7, 8
    }

    void AdvanceTour()
    {
        // Sequence: Bar -> Dining -> Lounge -> Library -> Garden -> Foyer -> Nothing
        Transform target = null;

        switch (travelStep)
        {
            case 0: target = barLoc; break;
            case 1: target = diningLoc; break;
            case 2: target = loungeLoc; break;
            case 3: target = libraryLoc; break;
            case 4: target = gardenLoc; break;
            case 5: target = foyerLoc; break;
            default: return; // "Nothing" - NPC stays in Foyer
        }

        if (target != null) transform.position = target.position;
        travelStep++;
    }
}