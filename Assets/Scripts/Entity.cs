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
    public float moveSpeedMultiplier = 1;
    public float jumpForceMultiplier = 1;
    public float maxVelocity = 7;

    internal Rigidbody2D rb;
    internal Collider2D cd;
    internal EntityState state;

    private PickupInteractable pickedObject;
    private float defaultMass;

    public PickupInteractable PickedObject
    {
        get
        {
            return pickedObject;
        }
        set
        {
            pickedObject = value;
            if(value)
            {
                rb.mass = defaultMass + pickedObject.rigidbody2D.mass;
            }
            else
            {
                rb.mass = defaultMass;
            }
        }
    }
    public float MoveSpeed
    {
        get
        {
            float finalMoveSpeed = moveSpeedMultiplier;

            return finalMoveSpeed;
        }
    }

    internal void Pickup(PickupInteractable pickupInteractable)
    {
        PickedObject = pickupInteractable;

        PickedObject.transform.SetParent(pickupParent);
        PickedObject.transform.localEulerAngles = pickupInteractable.pickupAngleOffset;
        PickedObject.transform.localPosition = pickupInteractable.pickupPosOffset;

        PickedObject.rigidbody2D.isKinematic = true;
    }
    internal void Drop()
    {
        PickedObject.transform.SetParent(PickedObject.defaultParent);
        PickedObject = null;

        PickedObject.rigidbody2D.isKinematic = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();

        defaultMass = rb.mass;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
