using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CostsScriptableObject", order = 1)]
public class CostsScriptableObject : ScriptableObject
{
    [SerializeField] int[] unitCosts = null;
    [SerializeField] int[] buildingCosts = null;

    public int GetUnitCosts(UnitType type)
    {
        return unitCosts[(int)type];
    }

    public int GetBuildingCosts(BuildingType type)
    {
        return buildingCosts[(int)type];
    }
}