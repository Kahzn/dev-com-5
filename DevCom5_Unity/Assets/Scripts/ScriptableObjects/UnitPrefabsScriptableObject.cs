using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UnitPrefabsScriptableObject", order = 1)]
public class UnitPrefabsScriptableObject : ScriptableObject
{
    public GameObject[] brightUnitPrefabs;
    public GameObject[] darkUnitPrefabs;
    public GameObject[] brightBuildingPrefabs;
    public GameObject[] darkBuildingPrefabs;
}