using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool followPlayer;
    [SerializeField] private float speed;
    private Vector2 currentPosition;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float speedFollowingPlayer;
    private float lookAhead;

    public void Update()
    {
        if (!followPlayer)
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosition.x, currentPosition.y, transform.position.z), ref velocity, speed);
        else
        {
            transform.position = new Vector3(playerTransform.position.x + lookAhead, transform.position.y, transform.position.z);
            lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * playerTransform.localScale.x), speedFollowingPlayer * Time.deltaTime);
        }
    }

    public void MoveToNewZone(Transform newZone)
    {
        if(newZone != null)
            currentPosition = newZone.position;
    }
}
