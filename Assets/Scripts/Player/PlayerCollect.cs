using System.Collections;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField]
    private AudioClip victorySound;

    private UIManager uIManager;
    private LevelTimer levelTimer;

    private void Awake()
    {
        uIManager = FindFirstObjectByType<UIManager>();
        levelTimer = FindFirstObjectByType<LevelTimer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == TagConstants.TagToolCollectible)
        {
            levelTimer.enabled = false;
            SoundManager.Instance.PlaySound(victorySound);
            collision.gameObject.SetActive(false);
            StartCoroutine(ShowGameEnding());
        }
    }
    private IEnumerator ShowGameEnding()
    {
        yield return new WaitForSeconds(1);
        uIManager.ShowGameEnd(true);
    }
}