using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateActivator : Activator
{
    bool isColliding = false;
    public override bool CanActivate()
    {
        return isColliding;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }
}
