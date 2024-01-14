using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractable : InteractableObject
{
    internal new Collider2D collider2D;
    internal new Rigidbody2D rigidbody2D;
    internal Transform defaultParent;

    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        defaultParent = transform.parent;
    }

    public override void OnPlayerInteract(Entity entity)
    {
        entity.Pickup(this);
    }
}
