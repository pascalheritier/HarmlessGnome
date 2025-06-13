using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool followPlayer;
    [SerializeField] private float speed;
    private float currentPositionX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float speedFollowingPlayer;
    private float lookAhead;

    public void Update()
    {
        if (!followPlayer)
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPositionX, transform.position.y, transform.position.z), ref velocity, speed);
        else
        {
            transform.position = new Vector3(playerTransform.position.x + lookAhead, transform.position.y, transform.position.z);
            lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * playerTransform.localScale.x), speedFollowingPlayer * Time.deltaTime);
        }
    }

    public void MoveToNewRoom(Transform newRoom)
    {
        currentPositionX = newRoom.position.x;
    }
}
