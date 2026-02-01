using UnityEngine;

public class NPCLogic : MonoBehaviour
{
    [Header("Identity")]
    public string npcName; // Set this in Inspector (e.g., "red", "doorman")

    // We change this to OnTriggerEnter2D because you said NPCs are triggers now
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Check if it's the player
        if (other.CompareTag("Player"))
        {
            // 2. Get the player's current data
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                // 3. Capture the data
                string who = npcName;
                string where = player.currentLocation;
                string mask = player.equippedMask;

                // 4. Print EXACTLY what you asked for
                Debug.Log($"Interaction: NPC={who}, Location={where}, Mask={mask}");
                
                // (Optional) Send to EndingManager here later
            }
        }
    }
}