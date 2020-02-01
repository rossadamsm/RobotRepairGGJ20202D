using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player1, player2;
    [SerializeField] private float camMoveSpeed = 10;

    private Vector3 targetLocation;

    private void Update()
    {
        targetLocation = new Vector3((player1.position.x + player2.position.x)/2f, (player1.position.y + player2.position.y)/2f, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, targetLocation, camMoveSpeed * Time.deltaTime);
    }
}
