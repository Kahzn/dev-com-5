using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Faction faction = Faction.Bright;
    private int numCollisions = 0;
    [SerializeField] List<int> toBuild;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        ++numCollisions;
        if (numCollisions == 1)
        {
            // shader red
        }
    }

    private void OnTriggerExit(Collider other)
    {
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
