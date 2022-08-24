using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [Tooltip("To change the boost direction, use a 1 or 0 in each x,y,z. So for forwards, use (0,01)")]
    //Default our boostDirection to up
    public Vector3 boostDirection = new Vector3(0, 1, 0);
    public float boostPower = 250;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("boost");
            other.attachedRigidbody.AddForce(boostDirection * boostPower);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
