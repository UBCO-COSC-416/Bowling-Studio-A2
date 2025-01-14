using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private BallController ball;
    [Header("Score Fields")]
    [SerializeField] private float score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Pin Fields")]
    [SerializeField] private GameObject pinCollection;
    [SerializeField] private Transform pinAnchor;

    private FallTrigger[] fallTriggers;
    private GameObject pinObjects;
    private InputManager inputManager;

    private void Awake()
    {
        ball = FindAnyObjectByType<BallController>();
        inputManager = GetComponent<InputManager>();
    }

    private void OnEnable()
    {
        inputManager.OnResetPressed.AddListener(HandleReset);
        SetPins();
    }

    private void OnDisable()
    {
        inputManager.OnResetPressed.RemoveListener(HandleReset);
        foreach (FallTrigger pin in fallTriggers)
        {
            pin.OnPinFall.RemoveListener(IncrementScore);
        }
    }

    private void SetPins()
    {
        pinObjects = Instantiate(pinCollection, pinAnchor.transform.position, Quaternion.identity, transform);
        fallTriggers = FindObjectsByType<FallTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (FallTrigger pin in fallTriggers)
        {
            pin.OnPinFall.AddListener(IncrementScore);
        }
    }

    private void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }

    private void HandleReset()
    {
        if(pinObjects)
        {
            foreach (Transform child in pinObjects.transform)
            {
                Destroy(child.gameObject);
            }

            Destroy(pinObjects);
        }
        ball.ResetBall();
        SetPins();
    }
}
