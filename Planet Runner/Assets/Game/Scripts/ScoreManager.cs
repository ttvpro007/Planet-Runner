using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] float scorePerSec = 0;
    [SerializeField] GameObject player = null;
    [SerializeField] OnScoreUpdatedEvent onScoreUpdated = null;

    [SerializeField] float score = 0;
    public float Score { get { return score; } }

    float TimeSinceIncrement = Mathf.Infinity;

    Health healthComp;

    private void Start()
    {
        healthComp = player.GetComponent<Health>();
    }

    private void Update()
    {
        if (TimeSinceIncrement >= 1)
        {
            if (healthComp.CurrentHealth != 0)
            {
                AddScore(scorePerSec);
                TimeSinceIncrement = 0;
            }
        }
        TimeSinceIncrement += Time.deltaTime;
    }

    private void AddScore(float scorePerSec)
    {
        score += scorePerSec;
        onScoreUpdated.Invoke(score);
    }
}

[System.Serializable]
public class OnScoreUpdatedEvent : UnityEvent<float>
{
}
