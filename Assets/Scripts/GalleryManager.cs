using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject cardPrefab;      // Your GalleryCard prefab
    public Transform gridContainer;    // The "Content" object with the Grid Layout

    private Sprite[] loadedSprites;

    void Start()
    {
        // 1. Procedurally load all images from the "Resources/GalleryArts" folder
        Debug.LogError("THE SCRIPT IS RUNNING!");
        loadedSprites = Resources.LoadAll<Sprite>("GalleryArts");

        Debug.Log("Loaded " + loadedSprites.Length + " images from Resources.");

        GenerateGallery();
    }

    void GenerateGallery()
    {
        // 2. Loop through exactly 16 slots (or however many images found)
        for (int i = 0; i < loadedSprites.Length; i++)
        {
            // Create the card
            GameObject newCard = Instantiate(cardPrefab, gridContainer);

            // Get the image component
            Image displayImage = newCard.GetComponent<Image>();
            
            // Assign the sprite from our auto-loaded array
            displayImage.sprite = loadedSprites[i];

            // 3. RANDOM DISPLAY LOGIC (User Request)
            // Pick a random number between 0.0 and 1.0
            float chance = Random.Range(0f, 1f);

            if (chance > 0.5f) 
            {
                // 50% chance: Show it normally
                displayImage.color = Color.white; 
            }
            else 
            {
                // 50% chance: Black it out (Locked)
                displayImage.color = Color.black;
            }
        }
    }
}