using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UnitPrefabsScriptableObject", order = 1)]
public class UnitPrefabsScriptableObject : ScriptableObject
{
    [SerializeField] private GameObject[] brightUnitPrefabs;
    [SerializeField] private GameObject[] darkUnitPrefabs;
    [SerializeField] private GameObject[] brightBuildingPrefabs;
    [SerializeField] private GameObject[] darkBuildingPrefabs;

    public GameObject GetUnitPrefab(Faction faction, UnitType unitType)
    {
        return (faction == Faction.Bright ? brightUnitPrefabs : darkUnitPrefabs)[(int)unitType];
    }

    public GameObject GetBuildingPrefab(Faction faction, BuildingType buildingType)
    {
        return (faction == Faction.Bright ? brightBuildingPrefabs : darkBuildingPrefabs)[(int)buildingType];
    }
}