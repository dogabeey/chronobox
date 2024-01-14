using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum EntityState
    {
        Idle,
        Run,
        Carry,
        Dead
    }

    public Transform pickupParent;

    internal Rigidbody2D rb;
    internal Collider2D cd;

    private PickupInteractable pickedObject;
    internal EntityState state;

    public PickupInteractable PickedObject
    {
        get
        {
            return pickedObject;
        }
        set
        {
            PickedObject = value;
        }
    }

    internal void Pickup(PickupInteractable pickupInteractable)
    {
        PickedObject = pickupInteractable;
        PickedObject.transform.SetParent(pickupParent);
    }
    internal void Drop()
    {
        PickedObject.transform.SetParent(PickedObject.defaultParent);
        PickedObject = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
