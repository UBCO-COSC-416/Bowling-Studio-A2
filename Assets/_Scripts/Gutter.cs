using UnityEngine;

public class Gutter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            BallLauncher ball = other.GetComponent<BallLauncher>();
            ball.BallGuttered(transform);
        }
    }
}
