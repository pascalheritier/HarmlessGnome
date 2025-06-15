using System.Collections;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    [SerializeField]
    private int totalLevelToolCollectibleCount;

    [SerializeField]
    private AudioClip victorySound;

    private UIManager uIManager;
    private LevelTimer levelTimer;
    private PlayerCollect playerCollect;

    private string[] level1StartDialogue =
    {
        "I wonder why all those people are afraid of garden gnomes, I swear I am perfectly harmless!",
        "The only thing I need right now are my gardening tools, let's find them quickly before this garden gets out of hand.",
        "Keyboard: WASD | Xbox controller: Joystick"
    };

    private void Awake()
    {
        uIManager = FindFirstObjectByType<UIManager>();
        levelTimer = FindFirstObjectByType<LevelTimer>();
        playerCollect = FindFirstObjectByType<PlayerCollect>();
        DialogueManager dialogueManager = FindFirstObjectByType<DialogueManager>(FindObjectsInactive.Include);
        dialogueManager.ShowDialogue(level1StartDialogue);
    }

    private void Update()
    {
        if(playerCollect.GetCurrentToolCollectibleCount() == totalLevelToolCollectibleCount)
        {
            levelTimer.enabled = false;
            SoundManager.Instance.PlaySound(victorySound);
            StartCoroutine(ShowGameEnding());
        }
    }

    private IEnumerator ShowGameEnding()
    {
        yield return new WaitForSeconds(0);
        uIManager.ShowGameEnd(true);
    }
}