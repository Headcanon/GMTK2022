using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : SingletonMonoBehaviour<ScoreTracker>
{
    [NonSerialized] public float distance;
     public float bestDistance;
     public float bestScore;

    public event Action<int, float> gameOver;

    private Vector3 startPoint;

    private void Start()
    {
        startPoint = transform.position;
        bestDistance = PlayerPrefs.GetFloat("BestDistance");
        bestScore = PlayerPrefs.GetFloat("BestScore");
        DiceManager.Instance.ShowResults += ShowResults;
    }

    private void Update()
    {
        distance = Vector3.Distance(startPoint, DiceManager.Instance.farthestDie.transform.position);
    }

    private void ShowResults(int finalValue, Vector3 finalPosition)
    {
        if (distance > bestDistance) PlayerPrefs.SetFloat("BestDistance", distance);

        float finalScore = finalValue * distance;
        if (finalScore > bestScore) PlayerPrefs.SetFloat("BestScore", finalScore);

        gameOver?.Invoke(finalValue, finalScore);
    }
    private void OnDisable()
    {
        if(DiceManager.Instance) DiceManager.Instance.ShowResults -= ShowResults;
    }
}
