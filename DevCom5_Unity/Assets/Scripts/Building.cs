using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private int numCollisions = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collide!!!!!");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("uncollide!!!!!");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide");
        ++numCollisions;
        if (numCollisions == 1)
        {
            // shader red
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("uncollide");
        --numCollisions;
        Debug.Assert(numCollisions >= 0);
        if (numCollisions == 0)
        {
            // shader normal
        }
    }

    public bool IsColliding()
    {
        return numCollisions > 0;
    }
}
