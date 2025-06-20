using System.Collections;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    [SerializeField]
    private AudioClip victorySound;

    [SerializeField]
    private LevelTimer levelTimer;

    [SerializeField]
    private LevelToolSpawner levelToolSpawner;

    private UIManager uiManager;
    DialogueManager dialogueManager;
    private PlayerCollect playerCollect;

    private string[] GetLevel1StartDialogue() => new string[]
    {
        "I wonder why all those people are afraid of garden gnomes, I swear I am perfectly harmless!",
        $"The only thing I need right now are my {levelToolSpawner.GetToolCollectibleTotalCount()} gardening tools, let's find them quickly before this garden gets out of hand.",
        "Keyboard: WASD | Xbox controller: Joystick"
    };

    private void Awake()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        playerCollect = FindFirstObjectByType<PlayerCollect>();
        dialogueManager = FindFirstObjectByType<DialogueManager>(FindObjectsInactive.Include);
    }

    private void Start()
    {
        dialogueManager.ShowDialogue(GetLevel1StartDialogue());
    }

    private void Update()
    {
        if (playerCollect.GetCurrentToolCollectibleCount() == levelToolSpawner.GetToolCollectibleTotalCount())
        {
            ManageGameEnding();
        }
        else if (levelTimer.IsTimeUp)
        {
            if (playerCollect.GetCurrentToolCollectibleCount() > 0)
                ManageGameEnding();
            else if (!uiManager.IsGameOverScreenShowing())
                uiManager.ShowGameOver();
        }
    }

    private void ManageGameEnding()
    {
        if (uiManager.IsDialogeBoxShowing())
            return; // wait until dialogbox is closed to show other screens
        levelTimer.enabled = false;
        SoundManager.Instance.PlaySound(victorySound);
        StartCoroutine(ShowGameEnding());
    }

    private IEnumerator ShowGameEnding()
    {
        yield return new WaitForSeconds(0.5f);
        uiManager.ShowGameEnd(true);
    }
}