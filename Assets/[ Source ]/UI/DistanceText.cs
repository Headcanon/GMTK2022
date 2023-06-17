using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private void Update()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = "Distance: " + string.Format("{0:0.00}", ScoreTracker.Instance.distance) + " m"; 
    }
}
