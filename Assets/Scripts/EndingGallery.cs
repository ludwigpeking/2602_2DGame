using UnityEngine;
using System.Collections.Generic; // Required for Dictionary

public class EndingGallery : MonoBehaviour
{
    [Header("UI Reference")]
    public SpriteRenderer endingRenderer; // Drag your Sprite object here

    [Header("Fallback Settings")]
    // Drag your "Default" or "Error" image here in the Inspector
    public Sprite backupImage;

    [Header("Debug Info")]
    public string finalEndingCode = "0000";
    public bool isSpecialEnding = false;

    void Start()
    {
        if (endingRenderer == null)
        {
            Debug.LogError("EndingGallery: No SpriteRenderer assigned!");
            return;
        }

        FindAndSetEnding();
    }

    void FindAndSetEnding()
    {
        // 1. Reset defaults
        string redDigit = "0";
        string blueDigit = "0";
        string yellowDigit = "0";
        string whiteDigit = "0";
        
        // 2. Retrieve Scores Safely (Defaults to 0 or "" if NPC is missing)
        int dHate = GetHate("Doorman");
        
        int rLove = GetLove("NPC_Red");    int rHate = GetHate("NPC_Red");    string rLoc = GetLoc("NPC_Red");
        int bLove = GetLove("NPC_Blue");   int bHate = GetHate("NPC_Blue");   string bLoc = GetLoc("NPC_Blue");
        int yLove = GetLove("NPC_Yellow"); int yHate = GetHate("NPC_Yellow"); string yLoc = GetLoc("NPC_Yellow");
        int wLove = GetLove("NPC_White");  int wHate = GetHate("NPC_White");  string wLoc = GetLoc("NPC_White");

        // =================================================================
        // PHASE 1: CHECK SPECIAL ENDINGS
        // =================================================================

        if (dHate == 1) 
            SetSpecial("1");

        else if (rHate == 1 && bHate == 1 && yHate == 1 && wHate == 1) 
            SetSpecial("2");
        
        else if (rHate == 2 && bHate == 2 && yHate == 2 && wHate == 2) 
            SetSpecial("3");

        // High Love Specials (12-15 range)
        else if (rLove > 11 && rLove < 16) SetSpecial("4");
        else if (bLove > 11 && bLove < 16) SetSpecial("5");
        else if (yLove > 11 && yLove < 16) SetSpecial("6");
        else if (wLove > 11 && wLove < 16) SetSpecial("7");

        // Location Matches (Only if Love < 6)
        else if (rLove < 6 && bLove < 6 && rLoc == bLoc && rLoc != "") SetSpecial("8");
        else if (rLove < 6 && yLove < 6 && rLoc == yLoc && rLoc != "") SetSpecial("9");
        else if (rLove < 6 && wLove < 6 && rLoc == wLoc && rLoc != "") SetSpecial("10");
        else if (bLove < 6 && yLove < 6 && bLoc == yLoc && bLoc != "") SetSpecial("11");
        else if (bLove < 6 && wLove < 6 && bLoc == wLoc && bLoc != "") SetSpecial("12");
        else if (yLove < 6 && wLove < 6 && yLoc == wLoc && yLoc != "") SetSpecial("13");

        // Perfect Love (16)
        else if (rLove == 16) SetSpecial("14");
        else if (bLove == 16) SetSpecial("15");
        else if (yLove == 16) SetSpecial("16");
        else if (wLove == 16) SetSpecial("17");


        // =================================================================
        // PHASE 2: GENERIC ENDINGS (If no special ending found)
        // =================================================================
        
        if (!isSpecialEnding)
        {
            // Red Logic
            if (rHate > 0) redDigit = "0";
            else if (rLove < 6) redDigit = "1";
            else if (rLove < 12) redDigit = "2";

            // Blue Logic
            if (bHate > 0) blueDigit = "0";
            else if (bLove < 6) blueDigit = "1";
            else if (bLove < 12) blueDigit = "2";

            // Yellow Logic
            if (yHate > 0) yellowDigit = "0";
            else if (yLove < 6) yellowDigit = "1";
            else if (yLove < 12) yellowDigit = "2";

            // White Logic
            if (wHate > 0) whiteDigit = "0";
            else if (wLove < 6) whiteDigit = "1";
            else if (wLove < 12) whiteDigit = "2";

            finalEndingCode = redDigit + blueDigit + yellowDigit + whiteDigit;
            LoadAndDisplayImage(finalEndingCode);
        }
    }

    // --- Helpers to make logic cleaner and prevent crashes ---

    void SetSpecial(string code)
    {
        finalEndingCode = code;
        isSpecialEnding = true;
        LoadAndDisplayImage(finalEndingCode);
    }

    void LoadAndDisplayImage(string imageName)
    {
        // 1. Try to load the specific ending from the Resources folder
        Sprite art = Resources.Load<Sprite>("Endings/" + imageName);

        if (art != null)
        {
            // SUCCESS: Found the specific file
            endingRenderer.sprite = art;
            Debug.Log($"EndingGallery: Successfully loaded '{imageName}'");
        }
        else
        {
            // FAILURE: File missing, use the backup
            Debug.LogWarning($"EndingGallery: Could not find 'Resources/Endings/{imageName}'. Using Backup.");
            
            if (backupImage != null)
            {
                endingRenderer.sprite = backupImage;
            }
            else
            {
                Debug.LogError("EndingGallery: CRITICAL - No specific image found AND no backup image assigned!");
            }
        }
    }

    // Safe Data Extractors
    int GetLove(string npc) { return GetIntScore(npc, "Love"); }
    int GetHate(string npc) { return GetIntScore(npc, "Hate"); }
    
    int GetIntScore(string npc, string type)
    {
        if (NPCBase.finalScores.ContainsKey(npc))
            return (int)NPCBase.finalScores[npc][type];
        return 0; // Default if NPC not met
    }

    string GetLoc(string npc)
    {
        if (NPCBase.finalScores.ContainsKey(npc))
            return (string)NPCBase.finalScores[npc]["Location"];
        return ""; // Default if NPC not met
    }
}