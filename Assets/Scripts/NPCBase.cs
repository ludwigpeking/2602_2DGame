using UnityEngine;
using System.Collections;
using System.Collections.Generic; // <--- ADD THIS LINE
using UnityEngine.SceneManagement;

public class NPCBase : MonoBehaviour
{
    [Header("Common Identity")]
    public string npcName;
    public int hateScore = 0; 
    public int loveScore = 0;
    public string currentRoom = "Unknown"; // Stores current room name

    private Vector3 originalScale;
    private bool isWiggling = false;

   
    public static Dictionary<string, Dictionary<string, object>> finalScores = new Dictionary<string, Dictionary<string, object>>();

    public static void SaveAllScores()
    {
        finalScores.Clear();
        NPCBase[] allNPCs = FindObjectsOfType<NPCBase>();
        foreach (NPCBase npc in allNPCs)
        {
            var data = new Dictionary<string, object>();
            data.Add("Love", npc.loveScore);
            data.Add("Hate", npc.hateScore);
            data.Add("Location", npc.currentRoom); // Save the location!
            
            finalScores.Add(npc.npcName, data);
        }
    }

    protected virtual void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // NEW: The "Hate Gate"
            // If hate is 2 or more, we exit the function immediately
            if (hateScore >= 2)
            {
                Debug.Log($"{npcName} is too angry to talk to you.");
                return; 
            }

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.RegisterInteraction();
                if (!isWiggling) StartCoroutine(EnlargeEffect());

                OnPlayerInteract(player);
            }
        }
    }

    protected virtual void OnPlayerInteract(PlayerController player) 
    { 
        // Logic filled by child scripts
    }

    IEnumerator EnlargeEffect()
    {
        isWiggling = true;
        transform.localScale = originalScale * 1.05f;
        yield return new WaitForSeconds(0.2f);
        transform.localScale = originalScale;
        isWiggling = false;
    }
}