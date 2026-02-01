using UnityEngine;
using UnityEngine.SceneManagement; // Needed to load the EndGame scene

public class NPCDoorman : NPCBase // Inherits from your new NPCBase
{
    [Header("Doorman Specifics")]
    public GameObject door;

    protected override void OnPlayerInteract(PlayerController player)
    {
        // We use .ToUpper() or .ToLower() to prevent the "Case Sensitivity" bug from before
        string mask = player.equippedMask.ToUpper();

        if (mask == "WHITE")
        {
            OpenTheDoor();
        }
        else if (mask == "BLUE")
        {
            TriggerEndGame();
        }
        else
        {
            Debug.Log("Doorman: 'Wrong mask, keep moving.'");
        }
    }

    void OpenTheDoor()
    {
        if (door != null)
        {
            door.SetActive(false);
            Debug.Log("Doorman: 'Welcome. The door is open.'");
        }
    }

    void TriggerEndGame()
    {
        Debug.Log("Doorman: 'A wise choice. It is over.'");
        SceneManager.LoadScene("EndGame");
    }
}