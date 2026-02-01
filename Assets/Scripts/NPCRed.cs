using UnityEngine;

public class NPCRed : NPCBase 
{


    [Header("Sequential Travel")]
    public Transform[] travelPoints; 
    private int currentPointIndex = 0; // Keeps track of where we are in the list

    protected override void OnPlayerInteract(PlayerController player)
    {
        string mask = player.equippedMask.ToUpper();

        if (mask == "RED") loveScore += 2;
        else if (mask == "BLUE") hateScore += 1;
        else if (mask == "WHITE") loveScore += 1;
        else if (mask == "YELLOW") TravelToNextPoint(); // Changed to sequential

        Debug.Log($"{npcName}: Love={loveScore}, Hate={hateScore}");
    }

    public void TravelToNextPoint()
    {
        if (travelPoints != null && travelPoints.Length > 0)
        {
            // 1. Move to the current point in the list
            transform.position = travelPoints[currentPointIndex].position;

            // 2. Advance the index for the NEXT time we bump
            currentPointIndex++;

            // 3. Reset to 0 if we reach the end of the list (Looping)
            if (currentPointIndex >= travelPoints.Length)
            {
                currentPointIndex = 0;
            }

            Debug.Log($"{npcName} moved to point {currentPointIndex}. Next stop: {currentPointIndex + 1}");
        }
        else
        {
            Debug.LogError("Travel Points list is empty!");
        }
    }
}