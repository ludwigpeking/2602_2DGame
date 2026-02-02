using UnityEngine;

public class RoomArea : MonoBehaviour
{
    [Header("Room Identity")]
    public string roomName; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Check for Player
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.currentLocation = roomName;
                Debug.Log("Player entered: " + roomName);
            }
        }

        // 2. Check for NPCs
        NPCBase npc = other.GetComponent<NPCBase>();
        if (npc != null)
        {
            npc.currentRoom = roomName;
            Debug.Log($"{npc.npcName} moved to: {roomName}");
        }
    }
}