using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] 
    private float health;

    [Header("SFX")]
    [SerializeField]
    private AudioClip collectedSound;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == TagConstants.TagPlayer)
        {
            SoundManager.Instance.PlaySound(collectedSound);
            collision.GetComponent<Health>().Heal(health);
            gameObject.SetActive(false);
        }
    }
}
