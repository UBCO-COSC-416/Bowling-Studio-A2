using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField] private Rigidbody ballRB;
    [SerializeField] private float force;
    
    private bool isLaunched = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballRB = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.Return) && !isLaunched)
        {
            ballRB.AddForce(transform.forward * force, ForceMode.Impulse);
            isLaunched = true;
        }

        if (ballRB.isKinematic)
        {
            transform.position += -Vector3.right * (Time.deltaTime * force);
        }
    }
}