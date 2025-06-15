using System.Collections;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField]
    private AudioClip itemPickupSound;

    private int toolCollectibleCounter;
    DialogueManager dialogueManager;

    private void Awake()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>(FindObjectsInactive.Include);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == TagConstants.TagToolCollectible)
        {
            toolCollectibleCounter++;
            SoundManager.Instance.PlaySound(itemPickupSound);
            var toolCollectible = collision.GetComponent<ToolCollectible>();
            dialogueManager.ShowDialogue(new string[] { toolCollectible.OnCollectedText });
            collision.gameObject.SetActive(false);
        }
    }

    public int GetCurrentToolCollectibleCount() => toolCollectibleCounter;
}