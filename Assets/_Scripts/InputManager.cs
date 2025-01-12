using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent OnSpacePressed = new UnityEvent();
    void Update()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            input += Vector2.up;
            Debug.Log($"User's Input: up");
        }
        if (Input.GetKey(KeyCode.S))
        {
            input += Vector2.down;
            Debug.Log($"User's Input: down");
        }
        if (Input.GetKey(KeyCode.A))
        {
            input += Vector2.left;
            Debug.Log($"User's Input: right");
        }
        if (Input.GetKey(KeyCode.D))
        {
            input += Vector2.right;
            Debug.Log($"User's Input: left");
        }
        OnMove?.Invoke(input);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke();
        }
    }
}
