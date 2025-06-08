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
    private bool playerControlsEnabled = true;
    private float horizontalInput;
    private float verticalInput;

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
        if (!playerControlsEnabled && !context.canceled) // always get key up, to stop motion even after disabling it
            return;

        if (context.canceled)
            StopMoving();

        Vector2 playerInput = context.ReadValue<Vector2>();
        if (playerInput.x != 0)
        {
            horizontalInput = playerInput.x;
            verticalInput = 0;
        }
        else if (playerInput.y != 0)
        {
            horizontalInput = 0;
            verticalInput = playerInput.y;
        }
        else
        {
            StopMoving();
        }
    }

    private void StopMoving()
    {
        horizontalInput = verticalInput = 0;
    }

    private void Update()
    {
        // Calling run here allows continuous horizontal movement when holding the motion input
        UpdateRunningBehaviour();
    }

    private void UpdateRunningBehaviour()
    {
        // Actual movement
        playerBody.linearVelocity = new Vector2(horizontalInput * runSpeed, verticalInput * runSpeed);

        // Visual animations
        ResetMotionAnimation();
        if (horizontalInput != 0)
        {
            playerAnimator.SetBool(AnimationConstants.Player.TransitionMovingSideways, true);
            // flip sprite left-right
            if (horizontalInput > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (verticalInput > 0)
        {
            playerAnimator.SetBool(AnimationConstants.Player.TransitionMovingUp, true);
        }
        else if (verticalInput < 0)
        {
            playerAnimator.SetBool(AnimationConstants.Player.TransitionMovingDown, true);
        }
    }

    private void ResetMotionAnimation()
    {
        playerAnimator.SetBool(AnimationConstants.Player.TransitionMovingSideways, false);
        playerAnimator.SetBool(AnimationConstants.Player.TransitionMovingUp, false);
        playerAnimator.SetBool(AnimationConstants.Player.TransitionMovingDown, false);
    }
}