using System.Collections;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField]
    private AudioClip itemPickupSound;

    private int toolCollectibleCounter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == TagConstants.TagToolCollectible)
        {
            toolCollectibleCounter++;
            SoundManager.Instance.PlaySound(itemPickupSound);
            collision.gameObject.SetActive(false);
        }
    }

    public int GetCurrentToolCollectibleCount() => toolCollectibleCounter;
}