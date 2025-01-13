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
        ball  = FindAnyObjectByType<BallLauncher>();
    }

    private void SpawnPins()
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
        if (resetRoutine != null)
        {
            Debug.Log("Stopping");
            StopCoroutine(resetRoutine);
        }
        score++;
        UpdateScore();
        isTimeToReset = false;
        resetRoutine = StartCoroutine(ResetCooldown());
    }

    private IEnumerator ResetCooldown()
    {
        isTimeToReset = true;
        yield return new WaitForSeconds(resetCooldown);
        if (isTimeToReset)
        {
            ResetLevel();
        }
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

    private void UpdateScore()
    {
        scoreText.text = $"Score: {score}";
    }

    private void OnDestroy()
    {
        foreach (FallTrigger pin in fallTriggers)
        {
            pin.OnPinFall.RemoveListener(IncrementScore);
        }
    }
}
