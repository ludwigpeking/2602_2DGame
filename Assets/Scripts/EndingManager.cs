using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// These templates sit OUTSIDE the main class
[System.Serializable] 
public class GameEvent
{
    public string npc;      // doorman, red, blue, yellow, white
    public string location; // Entry, Foyer, Ballroom, etc.
    public string inMask;   // red, blue, yellow, white, none
}

[System.Serializable]
public class EndingCondition
{
    public string endingName;
    public int endingID; // 0 to 15
    public List<GameEvent> requiredSequence; 
}

// This is the main class that attaches to your GameObject
public class EndingManager : MonoBehaviour
{
    public List<GameEvent> eventHistory = new List<GameEvent>();
    public List<EndingCondition> allEndings; 

    public void RecordInteraction(string npcName, string loc, string mask)
    {
        GameEvent newEvent = new GameEvent { npc = npcName, location = loc, inMask = mask };
        eventHistory.Add(newEvent);
        CheckForEndings();
    }

    void CheckForEndings()
    {
        foreach (var ending in allEndings)
        {
            if (IsSequenceMet(ending.requiredSequence))
            {
                TriggerEnding(ending.endingID);
                return;
            }
        }
    }

    bool IsSequenceMet(List<GameEvent> goal)
    {
        int goalIndex = 0;
        foreach (var historyEvent in eventHistory)
        {
            if (Matches(historyEvent, goal[goalIndex]))
            {
                goalIndex++;
                if (goalIndex >= goal.Count) return true; 
            }
        }
        return false;
    }

    bool Matches(GameEvent history, GameEvent criteria)
    {
        bool npcMatch = string.IsNullOrEmpty(criteria.npc) || history.npc == criteria.npc;
        bool locMatch = string.IsNullOrEmpty(criteria.location) || history.location == criteria.location;
        bool maskMatch = string.IsNullOrEmpty(criteria.inMask) || history.inMask == criteria.inMask;
        return npcMatch && locMatch && maskMatch;
    }

    void TriggerEnding(int id)
    {
        PlayerPrefs.SetInt("EndingUnlocked_" + id, 1);
        PlayerPrefs.SetInt("LastEndingSeen", id);
        SceneManager.LoadScene("EndingScreen"); // Matches your file name
    }
}