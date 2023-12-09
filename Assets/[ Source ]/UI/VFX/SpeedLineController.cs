using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedLineController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer image;
    private Color imageColor;

    private float maxSpeedReached = 0;
    private float currentSpeed = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<SpriteRenderer>();
        imageColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        GetSpeeds();
        image.color = new Color(imageColor.r, imageColor.g, imageColor.b, currentSpeed / maxSpeedReached);
    }

    private void GetSpeeds()
    {
        currentSpeed = DiceManager.Instance.farthestDieSpeed;
        if (currentSpeed > maxSpeedReached) maxSpeedReached = currentSpeed;
    }
}
