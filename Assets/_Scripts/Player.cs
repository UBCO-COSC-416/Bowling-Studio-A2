using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform anchor1, anchor2;
    [SerializeField] private InputManager inputManager;

    private float lerpParameter = 0.5f;

    private void Awake()
    {
        inputManager.OnMove.AddListener(OnMove);
    }
    private void OnMove(Vector2 direction)
    {
        if (direction == Vector2.right)
        {
            lerpParameter += Time.deltaTime;
        }
        else if (direction == Vector2.left)
        {
            lerpParameter -= Time.deltaTime;
        }
        lerpParameter = Mathf.Clamp(lerpParameter, 0, 1);
        transform.position = Vector3.Lerp(anchor1.position, anchor2.position, lerpParameter);
    }
}
