using UnityEngine;
using System.Collections;

public class NPCBase : MonoBehaviour
{
    [Header("Common Identity")]
    public string npcName;

    private Vector3 originalScale;
    private bool isWiggling = false;

    protected virtual void Start()
    {
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
                if (!isWiggling) StartCoroutine(EnlargeEffect());

                // Call the unique logic
                OnPlayerInteract(player);
            }
        }
    }

    // This is the "Empty Shell" that children will fill
    protected virtual void OnPlayerInteract(PlayerController player) 
    { 
        Debug.Log($"{npcName} interacted, but has no unique logic.");
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