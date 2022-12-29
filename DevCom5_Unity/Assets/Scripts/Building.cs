using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Faction faction = Faction.Bright;
    public Transform spawnLocation = null;
    private int numCollisions = 0;
    public List<int> toBuild;


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

    public UnitType GetUnitTypeByIndex(int index)
    {
        if (toBuild.Count - 1 > index) { Debug.LogError("Index out of range. provided index: " + index + " max index: " + (toBuild.Count - 1)); return UnitType.NONE; }
        else if (index < 0) { Debug.LogError("Index out of range. provided index: " + index + " min index: 0"); return UnitType.NONE; }

        else { return (UnitType)toBuild[index]; }
    }

    public bool HasUnitTypes()
    {
        return toBuild.Count > 0;
    }
}
