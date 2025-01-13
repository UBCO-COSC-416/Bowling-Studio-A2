using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody),typeof(InputManager))]
public class BallLauncher : MonoBehaviour
{
    public UnityEvent OnBallGuttered = new();
    
    [SerializeField] private float force;
    [SerializeField] private Transform ballAnchor;
    [SerializeField] private Transform launchIndicator;
    
    private BallState ballState = BallState.NotLaunched;
    private Rigidbody ballRB;
    private Transform gutter;
    private InputManager inputManager;
    void Start()
    {
        ballRB = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        Cursor.lockState = CursorLockMode.Locked;
        ResetBall();
    }
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
                ballRB.AddForce(launchIndicator.forward * force, ForceMode.Impulse);
                launchIndicator.gameObject.SetActive(false);
                ballState = BallState.Idle;
                break;
            case BallState.Idle:
                // Do Nothing
                break;
            case BallState.Gutter:
                float velocityMagnitude = ballRB.linearVelocity.magnitude;
                ballRB.linearVelocity = Vector3.zero;
                ballRB.angularVelocity = Vector3.zero;
                ballRB.AddForce(gutter.transform.forward * velocityMagnitude, ForceMode.VelocityChange);
                ballState = BallState.Idle;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public void ResetBall()
    {
        inputManager.OnSpacePressed.AddListener(OnBallLaunched);
        ballState = BallState.NotLaunched;
        launchIndicator.gameObject.SetActive(true);
    }
    
    public void BallGuttered(Transform T)
    {
        if (ballState == BallState.NotLaunched)
        {
            return;  
        }
        gutter = T;
        ballState = BallState.Gutter;
        OnBallGuttered?.Invoke();
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
    Idle = 2,
    Gutter = 3
}