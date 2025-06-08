using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float runSpeed;

    private PlayerInput playerInput;
    private Rigidbody2D playerBody;
    private bool playerControlsEnabled = true;
    private float horizontalInput;
    private float verticalInput;

    private void Awake()
    {
        TryGetComponent(out playerInput);
        TryGetComponent(out playerBody);
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
        {
            horizontalInput = verticalInput = 0;
        }
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
            horizontalInput = verticalInput = 0;
        }
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
    }
}