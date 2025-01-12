using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(InputManager))]
public class BallLauncher : MonoBehaviour
{
    
    [SerializeField] private float force;
    [SerializeField] private Transform ballAnchor;

    private BallState ballState = BallState.NotLaunched;
    private Rigidbody ballRB;
    private Transform gutter;
    private InputManager inputManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballRB = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        inputManager.OnSpacePressed.AddListener(OnBallLaunched);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        switch (ballState)
        {
            case BallState.NotLaunched:
                ballRB.isKinematic = true;
                ballRB.transform.position = ballAnchor.position;
                break;
            case BallState.OnLaunch:
                ballRB.isKinematic = false;
                ballRB.AddForce(transform.forward * force, ForceMode.Impulse);
                ballState = BallState.Launched;
                break;
            case BallState.Launched:
                break;
            case BallState.Gutter:
                float velocityMagnitude = ballRB.linearVelocity.magnitude;
                ballRB.linearVelocity = Vector3.zero;
                ballRB.angularVelocity = Vector3.zero;
                ballRB.AddForce(gutter.transform.forward * velocityMagnitude, ForceMode.VelocityChange);
                ballState = BallState.Launched;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    public void OnBallGuttered(Transform T)
    {
        gutter = T;
        ballState = BallState.Gutter;
    }

    private void OnBallLaunched()
    {
        ballState = BallState.OnLaunch;
        inputManager.OnSpacePressed.RemoveListener(OnBallLaunched);
    }
}

public enum BallState
{
    INVALID = -1,
    NotLaunched = 0,
    OnLaunch = 1,
    Launched = 2,
    Gutter = 3
}