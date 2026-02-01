using UnityEngine;
using UnityEngine.UI; // For Legacy Text
using System.Text;

public class ScoreDisplay : MonoBehaviour
{
    public Text displayText;

    void Start()
    {
        // 1. Check if the link is truly there
        if (displayText == null)
        {
            Debug.LogError("ScoreDisplay: The Text component is still NULL! Check the Inspector.");
            return;
        }

        // 2. Check if the Dictionary has data
        Debug.Log("ScoreDisplay: Checking dictionary... Entry count: " + NPCBase.finalScores.Count);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("=== FINAL NPC RELATIONSHIPS ===\n");

        if (NPCBase.finalScores.Count == 0)
        {
            sb.AppendLine("No interaction data found.");
            Debug.LogWarning("ScoreDisplay: The dictionary is empty!");
        }
        else
        {
            foreach (var entry in NPCBase.finalScores)
            {
                string line = $"{entry.Key}: {entry.Value}";
                sb.AppendLine(line);
                
                // 3. Print each score to the Console so we can see it regardless of the UI
                Debug.Log("Score Found: " + line);
            }
        }

        // 4. Update the UI
        displayText.text = sb.ToString();
        
        // 5. Visual Debug: Force the color to Red so we can see if it's just hidden
        // displayText.color = Color.red; 
    }
}