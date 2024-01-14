using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public string playerTag;
    public string interactString;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && other.TryGetComponent(out Entity entity))
        {
            OnPlayerEnterRange(entity);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag) && other.TryGetComponent(out Entity entity))
        {
            OnPlayerExitRange(entity);
        }
    }
    public virtual void OnPlayerEnterRange(Entity entity)
    {
        EventManager.TriggerEvent(Const.GameEvents.PLAYER_ENTERED_RANGE, new EventParam(paramObj: gameObject));
    }
    public virtual void OnPlayerExitRange(Entity entity)
    {
        EventManager.TriggerEvent(Const.GameEvents.PLAYER_EXITED_RANGE, new EventParam(paramObj: gameObject));
    }

    public abstract void OnPlayerInteract(Entity entity);
}
