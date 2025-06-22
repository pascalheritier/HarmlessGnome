using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float runSpeed;

    private PlayerInput playerInput;
    private Animator playerAnimator;
    private Rigidbody2D playerBody;
    private Vector2 motionDirection;

    private void Awake()
    {
        TryGetComponent(out playerInput);
        TryGetComponent(out playerBody);
        TryGetComponent(out playerAnimator);
        playerInput.onActionTriggered += context =>
        {
            if (context.action.name == InputActionConstants.Player.InputActionMove)
                OnMove(context);
        };
    }

    private void OnMove(CallbackContext context)
    {
        if (!enabled && !context.canceled) // always get key up, to stop motion even after disabling it
            return;

        if (context.canceled)
            StopMoving();

        Vector2 playerInput = context.ReadValue<Vector2>();
        if (playerInput.x != 0)
        {
            motionDirection.x = playerInput.x;
            motionDirection.y = 0;
        }
        else if (playerInput.y != 0)
        {
            motionDirection.x = 0;
            motionDirection.y = playerInput.y;
        }
        else
        {
            StopMoving();
        }
    }

    private void StopMoving()
    {
        motionDirection = Vector2.zero;
    }

    private void Update()
    {
        // Calling run here allows continuous horizontal movement when holding the motion input
        UpdateRunningBehaviour();
    }

    private void UpdateRunningBehaviour()
    {
        // Actual movement
        playerBody.linearVelocity = new Vector2(motionDirection.x * runSpeed, motionDirection.y * runSpeed);

        // Visual animations
        playerAnimator.SetBool(AnimationConstants.Player.TransitionMoving, motionDirection.magnitude > 0);
        if (motionDirection.x != 0)
        {
            playerAnimator.SetInteger(AnimationConstants.Player.TransitionMovingDirection, 1);
            // flip sprite left-right
            if (motionDirection.x > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (motionDirection.y > 0)
        {
            playerAnimator.SetInteger(AnimationConstants.Player.TransitionMovingDirection, 2);
        }
        else if (motionDirection.y < 0)
        {
            playerAnimator.SetInteger(AnimationConstants.Player.TransitionMovingDirection, 0);
        }
    }
}