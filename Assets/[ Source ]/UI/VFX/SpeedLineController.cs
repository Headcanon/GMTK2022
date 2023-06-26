using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedLineController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer image;
    private Color imageColor;

    void Start()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<SpriteRenderer>();
        imageColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = DiceManager.Instance.farthestDieSpeed;
        animator.SetFloat("SpeedMultiplier", (speed / 2) * -1);
        image.color = new Color(imageColor.r, imageColor.g, imageColor.b, speed / 6);
    }
}
