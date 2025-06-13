using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    private string[] level1StartDialogue =
    {
        "I wonder why all those people are afraid of garden gnomes, I swear I am perfectly harmless!",
        "The only thing I need right now are my gardening tools, let's find them quickly before this garden gets out of hand.",
        "Keyboard: WASD | Xbox controller: Joystick"
    };

    private void Awake()
    {
        DialogueManager dialogueManager = FindFirstObjectByType<DialogueManager>(FindObjectsInactive.Include);
        dialogueManager.ShowDialogue(level1StartDialogue);
    }
}