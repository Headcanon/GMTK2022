using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody die = other.GetComponent<Rigidbody>();
        if (die)
        {
            die.AddForce(Vector3.forward * 5, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}
