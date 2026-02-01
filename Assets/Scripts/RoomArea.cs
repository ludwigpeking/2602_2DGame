using UnityEngine;

public class RoomArea : MonoBehaviour
{
    [Header("Room Identity")]
    // Type the name exactly as you want it (e.g., "Library", "Bar")
    public string roomName; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Check if the object is the Player
        if (other.CompareTag("Player"))
        {
            // 2. Get the player's script
            PlayerController player = other.GetComponent<PlayerController>();
            
            // 3. Update the player's location memory
            if (player != null)
            {
                player.currentLocation = roomName;
                Debug.Log("Entered Room: " + roomName);
            }
        }
    }
}