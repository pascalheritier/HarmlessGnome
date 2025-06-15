using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 currentPosition;
    private Vector3 velocity = Vector3.zero;

    public void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosition.x, currentPosition.y, transform.position.z), ref velocity, speed);
    }

    public void MoveToNewZone(Transform newZone)
    {
        if (newZone != null)
        {
            Debug.Log($"Move camera to new zone (X:{newZone.position.x}, Y:{newZone.position.y})");
            currentPosition = newZone.position;
        }
    }
}
