using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private FallTrigger[] pins;
    [SerializeField] private float score = 0;
    private void Awake()
    {
        pins = FindObjectsByType<FallTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (FallTrigger pin in pins)
        {
            pin.OnPinFall.AddListener(IncrementScore);
        }
    }

    private void IncrementScore()
    {
        score++;
    }

    private void OnDestroy()
    {
        foreach (FallTrigger pin in pins)
        {
            pin.OnPinFall.RemoveListener(IncrementScore);
        }
    }
}
