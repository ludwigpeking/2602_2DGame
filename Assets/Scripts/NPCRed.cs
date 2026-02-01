using UnityEngine;

public class NPCRed : NPCBase // Inherits from NPCBase
{
    public int loveScore = 0;
    public int hateScore = 0;
    public Transform[] travelPoints;

    // We use "override" to fill the shell we created in the base script
    protected override void OnPlayerInteract(PlayerController player)
    {
        string mask = player.equippedMask.ToLower();

        if (mask == "red") loveScore += 2;
        else if (mask == "blue") hateScore += 1;
        else if (mask == "white") loveScore += 1;
        else if (mask == "yellow") Teleport();

        Debug.Log($"NPC_Red: Love={loveScore}, Hate={hateScore}");
    }

    void Teleport()
    {
        if (travelPoints.Length > 0)
            transform.position = travelPoints[Random.Range(0, travelPoints.Length)].position;
    }
}