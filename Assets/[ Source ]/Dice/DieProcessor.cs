using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieProcessor : MonoBehaviour
{
    public enum Buff { Impulse, GhostForm, Revivify, BounceBonus}
    public Buff dieBuff = Buff.Impulse;

    [SerializeField] private float impulseBonus = 5;
    [SerializeField] private float checkDistance = 1;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float initialForce = 10;


    public int value { get; private set; }
    public Rigidbody rigidbody;
    public Collider collider;
    public static event Action<int, DieProcessor> completeRoll;

    private Renderer renderer;
    private TrailRenderer trail;
    private bool finishedRoll = true;
    private DieSide[] dieSides = new DieSide[6];

    void Awake()
    {
        if(!collider) collider = GetComponent<Collider>();
        if (!rigidbody) rigidbody = GetComponent<Rigidbody>();
        if (!renderer) renderer = transform.GetChild(0).GetComponent<Renderer>();
        if (!trail) trail = GetComponent<TrailRenderer>();

        rigidbody.isKinematic = true;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("GhostForm"), LayerMask.NameToLayer("Obstacle"));
    }

    public void Roll()
    {
        finishedRoll = false;
        rigidbody.isKinematic = false;
        rigidbody.AddForce(Vector3.forward * initialForce, ForceMode.Impulse);
    }

    private void GenerateSides()
    {
        dieSides[0] = new DieSide(transform.forward, 1);
        dieSides[1] = new DieSide(-transform.right, 2);
        dieSides[2] = new DieSide(-transform.forward, 3);
        dieSides[3] = new DieSide(transform.right, 4);
        dieSides[4] = new DieSide(transform.up, 5);
        dieSides[5] = new DieSide(-transform.up, 6);
    }

    void FixedUpdate()
    {
        if(transform.position.y < -2) ProcessResult();
        else if ((rigidbody.IsSleeping() && aptToRoll)) ProcessResult();
        trail.time = rigidbody.velocity.magnitude / 2;
    }

    private void ProcessResult()
    {
        int finalValue = 1;
        GenerateSides();

        foreach (DieSide side in dieSides)
        {
            bool sideIsOppositeToGround = Physics.Raycast(transform.position, -side.checkDirection, checkDistance, groundLayer);
            if (sideIsOppositeToGround)
            {
                finalValue = side.value;
            }
        }
        value = finalValue;
        completeRoll?.Invoke(finalValue, this);
        finishedRoll = true;
    }

    private class DieSide
    {
        public int value { get; private set; }
        public Vector3 checkDirection { get; private set; }
        public DieSide(Vector3 newDirection, int newValue)
        {
            value = newValue;
            checkDirection = newDirection;
        }
    }

    public bool ReceiveBuff(DieProcessor deadDie)
    {
        CameraShaker.Instance.Shake();
        switch (deadDie.dieBuff)
        {
            case Buff.Impulse:
                {
                    Vector3 impulseDirection = (transform.position - deadDie.transform.position + Vector3.up).normalized;
                    rigidbody.AddForce(impulseDirection * impulseBonus * deadDie.value, ForceMode.Impulse);
                    return true;
                    break;
                }
            case Buff.GhostForm:
                {
                    StartCoroutine(GhostForm(deadDie.value));
                    return true;
                    break;
                }
            case Buff.Revivify:
                {
                    if (deadDie.value > 3)
                    {
                        deadDie.Revive();
                        return false;
                    }
                    else return true;
                    break;
                }
            case Buff.BounceBonus:
                {
                    collider.material.bounciness *= (deadDie.value / 10);
                    renderer.material.color += Color.green;
                    return true;
                    break;
                }
        }
        return true;
    }

    private bool revived = false;
    private bool aptToRoll { get { return !finishedRoll || revived; } }
    private void Revive()
    {
        revived = true;
        rigidbody.AddForce((Vector3.forward + Vector3.up) * impulseBonus, ForceMode.Impulse);
        rigidbody.AddTorque((Vector3.forward + Vector3.up) * impulseBonus, ForceMode.Impulse);
    }

    private IEnumerator GhostForm(int value)
    {
        gameObject.layer = LayerMask.NameToLayer("GhostForm");
        Color newColor = renderer.material.color;
        newColor.a = .5f;
        renderer.material.color = newColor;

        yield return new WaitForSeconds(value);
        gameObject.layer = LayerMask.NameToLayer("Default");

        newColor.a = 1f;
        renderer.material.color = newColor;
    }
}
