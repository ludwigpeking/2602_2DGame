using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NPCLogic : MonoBehaviour
{
    [Header("Identity")]
    public string npcName;
    public GameObject doorToOpen; // Link the door GameObject here (for Doorman)

    [Header("Visual Juice")]
    private Vector3 originalScale;
    private bool isWiggling = false;

    void Start()
    {
        // Save the scale at the very beginning
        originalScale = transform.localScale;
    }

private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.RegisterInteraction(); 

                // ... keep your visual juice and Doorman logic below ...
                if (!isWiggling) StartCoroutine(EnlargeEffect());
                CheckSpecialInteraction(player);
            }
        }
    }

    IEnumerator EnlargeEffect()
    {
        isWiggling = true;

        // Scale up to 105%
        transform.localScale = originalScale * 1.05f;

        // Wait for 0.2 seconds
        yield return new WaitForSeconds(0.2f);

        // Snap back to original scale
        transform.localScale = originalScale;

        isWiggling = false;
    }

    void CheckSpecialInteraction(PlayerController player)
    {
        if (npcName == "Doorman")
        {
            string mask = player.equippedMask; 

            // Handle the Blue Mask ending
            if (mask == "Blue")
            {
                Debug.Log("Doorman: 'A wise choice.' (Loading EndGame...)");
                LoadEndGameScene();
            }
            else if (mask == "White")
            {
                OpenTheDoor();
            }
            // Red and Yellow do nothing
        }
    }

    void LoadEndGameScene()
    {
        // This must match the EXACT name of your scene file in the Project window
        SceneManager.LoadScene("EndGame");
    }

    void OpenTheDoor()
    {
        if (doorToOpen != null)
        {
            // Simple way to "Open" it: Disable the object so player can walk through
            doorToOpen.SetActive(false); 
            
            // OR: Move it smoothly (Optional for later)
            // doorToOpen.transform.Translate(Vector3.up * 2);
        }
        else
        {
            Debug.LogError("You forgot to link the Door in the Inspector!");
        }
    }
}