using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using static UnityEngine.InputSystem.InputAction;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dialogueUiComponent;

    [SerializeField]
    private float textSpeed;

    private string[] dialogueTextLines;
    private int textIndex;
    private PlayerInput playerInput;
    private UIManager uiManager;

    public void ShowDialogue(string[] dialogueInput)
    {
        dialogueTextLines = dialogueInput;
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        uiManager = GetComponentInParent<UIManager>();
        playerInput = FindAnyObjectByType<PlayerInput>();
        playerInput.onActionTriggered += (context) =>
        {
            if (context.action.name == InputActionConstants.UI.InputActionSubmit)
            {
                if (uiManager.IsPauseScreenShowing() || uiManager.IsGameOverScreenShowing())
                    return; // do not submit when outside of dialogue box context

                OnSubmit(context);
            }
        };
    }

    private void OnEnable()
    {
        Debug.Log("Dialogue box is pausing game");
        uiManager.PauseGame(true);
        dialogueUiComponent.text = string.Empty;
        StartDialogue();
    }

    private void OnSubmit(CallbackContext context)
    {
        if (!context.started)
            return;

        if(dialogueUiComponent.text == dialogueTextLines[textIndex])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            dialogueUiComponent.text = dialogueTextLines[textIndex];
        }
    }

    private void StartDialogue()
    {
        textIndex = 0;
        StartCoroutine(TypeLines());
    }

    private IEnumerator TypeLines()
    {
        foreach (char character in dialogueTextLines[textIndex].ToCharArray())
        {
            dialogueUiComponent.text += character;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    private void NextLine()
    {
        if(textIndex < dialogueTextLines.Length -1)
        {
            textIndex++;
            dialogueUiComponent.text = string.Empty;
            StartCoroutine(TypeLines());
        }
        else
        {
            gameObject.SetActive(false);
            Debug.Log("Dialogue box is unpausing game");
            uiManager.PauseGame(false);
        }
    }
}
