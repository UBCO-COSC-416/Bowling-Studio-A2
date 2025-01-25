using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            BallController ball = other.GetComponent<BallController>();
            if (!ball.IsBallLaunched) return;
            Rigidbody ballRB = ball.GetComponent<Rigidbody>();
            float velocityMagnitude = ballRB.linearVelocity.magnitude;
            ballRB.linearVelocity = Vector3.zero;
            ballRB.angularVelocity = Vector3.zero;
            ballRB.AddForce(transform.forward * velocityMagnitude, ForceMode.VelocityChange);
        }
    }
}
