using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject cardPrefab;      
    public Transform gridContainer;    

    private Sprite[] loadedSprites;

    void Start()
    {
        // Load all images
        loadedSprites = Resources.LoadAll<Sprite>("GalleryArts");
        Debug.Log("Loaded " + loadedSprites.Length + " images.");

        GenerateGallery();
    }

    void GenerateGallery()
    {
        for (int i = 0; i < loadedSprites.Length; i++)
        {
            // Create the card
            GameObject newCard = Instantiate(cardPrefab, gridContainer);

            // Set the image
            Image displayImage = newCard.GetComponent<Image>();
            displayImage.sprite = loadedSprites[i];
            
            // Force it to be visible (White)
            displayImage.color = Color.white; 
        }
    }
}