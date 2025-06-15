using UnityEngine;

public enum EnumPlayerDirection
{
    Downwards,
    Left,
    Upwards,
    Right
}

public class GatewayBetweenZones : Gateway
{
    [SerializeField]
    private Transform newZone;

    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private EnumPlayerDirection playerDirectionTriggering;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == TagConstants.TagPlayer)
        {
            if (CheckIfEnteringNewZone(collision.transform))
                cameraController.MoveToNewZone(newZone);
            else if(CheckIfEnteringPreviousZone(collision.transform))
                cameraController.MoveToNewZone(previousZone);
        }
    }

    private bool CheckIfEnteringNewZone(Transform playerTransform) => playerDirectionTriggering switch
    {
        EnumPlayerDirection.Downwards => playerTransform.position.y > transform.position.y,
        EnumPlayerDirection.Left => playerTransform.position.x > transform.position.x,
        EnumPlayerDirection.Upwards => playerTransform.position.y < transform.position.y,
        EnumPlayerDirection.Right => playerTransform.position.x < transform.position.x,
        _ => false
    };

    private bool CheckIfEnteringPreviousZone(Transform playerTransform) => playerDirectionTriggering switch
    {
        EnumPlayerDirection.Downwards => playerTransform.position.y < transform.position.y,
        EnumPlayerDirection.Left => playerTransform.position.x < transform.position.x,
        EnumPlayerDirection.Upwards => playerTransform.position.y > transform.position.y,
        EnumPlayerDirection.Right => playerTransform.position.x > transform.position.x,
        _ => false
    };
}