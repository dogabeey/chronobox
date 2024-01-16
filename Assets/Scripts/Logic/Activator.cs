using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Activator : MonoBehaviour
{
    public List<Activatable> outputs = new List<Activatable>();
    public bool isActivated = false;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        foreach (Activatable output in outputs)
        {
            output.registeredActivators.Add(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CanActivate() && !isActivated)
        {
            if(animator) animator.SetTrigger("Activate");
            OnActivate();
        }
        if(!CanActivate() && isActivated)
        {
            if(animator) animator.SetTrigger("Deactivate");
            OnDeactivate();
        }
    }

    public abstract bool CanActivate();

    public virtual void OnActivate()
    {
        isActivated = true;
        foreach (Activatable output in outputs)
        {
            output.OnActivate();
        }
    }
    public virtual void OnDeactivate()
    {
        isActivated = false;
        foreach (Activatable output in outputs)
        {
            output.OnDeactivate();
        }
    }
}
