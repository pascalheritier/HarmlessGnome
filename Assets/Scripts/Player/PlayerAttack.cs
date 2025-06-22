using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private PlayerInput playerInput;
    private Animator playerAnimator;

    private void Awake()
    {
        TryGetComponent(out playerInput);
        TryGetComponent(out playerAnimator);
        playerInput.onActionTriggered += context =>
        {
            if (context.action.name == InputActionConstants.Player.InputActionAttack)
                OnAttack(context);
        };
    }

    private void OnAttack(CallbackContext context)
    {
        if (!enabled && !context.canceled) // always get key up, to stop motion even after disabling it
            return;

        if(context.started)
            playerAnimator.SetTrigger(AnimationConstants.Player.TransitionAttacking);
    }

    public int GetCurrentAttackPoints() => 0;
}
