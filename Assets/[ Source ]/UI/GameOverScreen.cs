using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distanceTextMesh;
    [SerializeField] private TextMeshProUGUI bestDistanceTextMesh;

    [SerializeField] private TextMeshProUGUI multiplierTextMesh;

    [SerializeField] private TextMeshProUGUI scoreTextMesh;
    [SerializeField] private TextMeshProUGUI bestScoreTextMesh;

    private void Start()
    {
        ScoreTracker.Instance.gameOver += OpenGameOverScreen;
    }

    private void OpenGameOverScreen(int finalValue, float finalScore)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        distanceTextMesh.text = "Distance: " + string.Format("{0:0.00}", ScoreTracker.Instance.distance) + " m";
        bestDistanceTextMesh.text = "Best Distance: " + string.Format("{0:0.00}", PlayerPrefs.GetFloat("BestDistance")) + " m";

        multiplierTextMesh.text = "x " + finalValue.ToString();

        scoreTextMesh.text = "Final Score: " + string.Format("{0:0.00}", finalScore);
        bestScoreTextMesh.text = "Best Score: " + string.Format("{0:0.00}", PlayerPrefs.GetFloat("BestScore"));

        ScoreTracker.Instance.gameOver -= OpenGameOverScreen;
    }
}
