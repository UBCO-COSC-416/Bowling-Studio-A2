using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;

    private Rigidbody rb;

    private void Awake()
    {
        inputManager.OnMove.AddListener(OnMove);
        rb = GetComponent<Rigidbody>();
    }

    private void OnMove(Vector2 direction)
    {
        // convert the direction vector's x and y components into a 3d vector
        // incase our player needs to move forward and backwards later on
        Vector3 moveDirection = new(direction.x, 0f, direction.y);
        rb.AddForce(speed * moveDirection);
    }
}
