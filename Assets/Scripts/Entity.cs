using System;
using System.Collections;
using System.Linq;
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
    public Transform dropParent;
    public float moveSpeedMultiplier = 1;
    public float jumpForceMultiplier = 1;
    public float maxVelocity = 7;

    internal Rigidbody2D rb;
    internal Collider2D cd;
    internal EntityState state;

    private PickupInteractable pickedObject;
    private List<PickupInteractable> pickupableObjects = new List<PickupInteractable>();
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

    private void OnEnable()
    {
        EventManager.StartListening(Const.GameEvents.PLAYER_ENTERED_RANGE, OnPlayerEnteredRange);
        EventManager.StartListening(Const.GameEvents.PLAYER_EXITED_RANGE, OnPlayerExitedRange);
    }
    private void OnDisable()
    {
        EventManager.StopListening(Const.GameEvents.PLAYER_ENTERED_RANGE, OnPlayerEnteredRange);
        EventManager.StopListening(Const.GameEvents.PLAYER_EXITED_RANGE, OnPlayerExitedRange);
    }
    private void OnPlayerEnteredRange(EventParam param)
    {
        pickupableObjects.Add(param.paramObj.GetComponent<PickupInteractable>());
    }
    private void OnPlayerExitedRange(EventParam param)
    {
        pickupableObjects.Remove(param.paramObj.GetComponent<PickupInteractable>());
    }

    //Pickup closest pickable object in terms of distance
    public void PickupClosest()
    {
        if(pickupableObjects.Count > 0)
        {
            float distance = pickupableObjects.Min(x => Vector2.Distance(x.transform.position, transform.position));
            Pickup(pickupableObjects.Find(x => Vector2.Distance(x.transform.position, transform.position) == distance));
        }
    }


    internal void Pickup(PickupInteractable pickupInteractable)
    {
        PickedObject = pickupInteractable;

        PickedObject.transform.SetParent(pickupParent);
        PickedObject.transform.localEulerAngles = pickupInteractable.pickupAngleOffset;
        PickedObject.transform.localPosition = pickupInteractable.pickupPosOffset;

        PickedObject.rigidbody2D.isKinematic = true;
        PickedObject.collider2D.enabled = false;

        EventManager.TriggerEvent(Const.GameEvents.PLAYER_PICKED_OBJECT, new EventParam(paramObj: PickedObject.gameObject));
    }
    internal void Drop()
    {
        EventManager.TriggerEvent(Const.GameEvents.PLAYER_DROPPED_OBJECT, new EventParam(paramObj: PickedObject.gameObject));

        PickedObject.transform.position = dropParent.position;
        PickedObject.transform.eulerAngles = Vector3.zero;
        PickedObject.transform.SetParent(PickedObject.defaultParent);

        PickedObject.rigidbody2D.isKinematic = false;
        PickedObject.collider2D.enabled = true;

        PickedObject = null;

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
