using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float resetCooldown;
    [SerializeField] private BallLauncher ball;
    [Header("Score Fields")]
    [SerializeField] private float score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Pin Fields")] 
    [SerializeField] private GameObject pinCollection;
    [SerializeField] private Transform pinAnchor;
    
    private FallTrigger[] fallTriggers;
    private GameObject pinObjects;
    private bool isTimeToReset;
    private Coroutine resetRoutine;
    private void Awake()
    {
        SpawnPins();
        ball = FindAnyObjectByType<BallLauncher>();
        ball.OnBallGuttered.AddListener(HandleReset);
    }

    private void SpawnPins()
    {
        pinObjects = Instantiate(pinCollection, pinAnchor.transform.position, Quaternion.identity, transform);
        fallTriggers = FindObjectsByType<FallTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (FallTrigger pin in fallTriggers)
        {
            pin.OnPinFall.AddListener(IncrementScore);
            pin.OnPinFall.AddListener(HandleReset);
        }  
    }

    private void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }

    private void HandleReset()
    {
        if (resetRoutine != null)
        {
            StopCoroutine(resetRoutine);
        }
        resetRoutine = StartCoroutine(ResetCooldown());
    }
    
    private IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(resetCooldown);
        ResetLevel();
    }

    private void ResetLevel()
    {
        foreach (Transform child in pinObjects.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(pinObjects);
        ball.ResetBall();
        SpawnPins();
    }
    
    private void OnDestroy()
    {
        foreach (FallTrigger pin in fallTriggers)
        {
            pin.OnPinFall.RemoveListener(IncrementScore);
            pin.OnPinFall.RemoveListener(HandleReset);
        }
        ball.OnBallGuttered.RemoveListener(HandleReset);
    }
}
