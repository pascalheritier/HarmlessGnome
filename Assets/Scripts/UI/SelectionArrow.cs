using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] 
    RectTransform[] options;

    [Header("SFX")]
    [SerializeField] 
    private AudioClip changeSound;

    [Header("SFX")]
    [SerializeField] 
    private AudioClip interactSound;


    private PlayerInput playerInput;
    RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        playerInput = FindAnyObjectByType<PlayerInput>();
        rect = GetComponent<RectTransform>();
        ChangeSelectedTextColor();

        playerInput.onActionTriggered += (context) =>
        {
            if (context.action.name == InputActionConstants.UI.InputActionNavigate)
                OnNavigate(context);
            else if (context.action.name == InputActionConstants.UI.InputActionSubmit)
                OnSubmit(context);
        };
    }

    private void OnNavigate(CallbackContext context)
    {
        if (context.control.device is Mouse || !context.started)
            return;
        Vector2 uiInput = context.ReadValue<Vector2>();
        bool downwardDirection = uiInput.y < 0;
        if (downwardDirection)
            ChangePosition(1);
        else
            ChangePosition(-1);
    }

    private void OnSubmit(CallbackContext context)
    {
        if (context.control.device is Mouse || !context.started)
            return;
        InteractWithOption();
    }

    private void ChangePosition(int change)
    {
        if (change == 0)
            return;

       SoundManager.Instance.PlaySound(changeSound);

        currentPosition += change;
        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length - 1)
            currentPosition = 0;

        ChangeSelectedTextColor();
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, rect.position.z);
    }

    private void ChangeSelectedTextColor()
    {
        var selectedOption = options[currentPosition];
        selectedOption.GetComponent<TextMeshProUGUI>().color = Color.red;
        foreach (var unselectedOption in options.Where(o => o != selectedOption))
        {
            unselectedOption.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
    }

    private void InteractWithOption()
    {
        SoundManager.Instance.PlaySound(interactSound);
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
