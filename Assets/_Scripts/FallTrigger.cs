using System;
using UnityEngine;
using UnityEngine.Events;

public class FallTrigger : MonoBehaviour
{
    public UnityEvent OnPinFall;
    public bool isPinFallen = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") && !isPinFallen)
        {
            isPinFallen = true;
            OnPinFall?.Invoke();
        }
    }
}
