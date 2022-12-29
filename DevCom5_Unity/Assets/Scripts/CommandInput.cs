using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CommandInput
{
    public UnitPrefabsScriptableObject unitPrefabs = null;

    public GameObject BuildUnit(Building building, UnitType type)
    {
        var spawnLocation = building.spawnLocation;
        var prefab = GameManager.Instance.prefabCollection.GetUnitPrefab(building.faction, type);        
        return GameObject.Instantiate(prefab, spawnLocation.transform.position, Quaternion.identity);
    }

    public void MoveUnit(GameObject unit, Vector3 targetPosition)
    {
        var agent = unit.GetComponentInChildren<NavMeshAgent>();
        Debug.Assert(agent != null);
        agent.SetDestination(targetPosition);
    }

    public void Attack(GameObject attacker, GameObject target)
    {
        // todo
    }

    public GameObject BuildBuilding(BuildingType type, Faction faction, Vector3 position)
    {
        var prefab = GameManager.Instance.prefabCollection.GetBuildingPrefab(faction, type);        
        Debug.Assert(prefab != null);
        var building = GameManager.Instantiate(prefab, position, Quaternion.identity);
        building.GetComponentInChildren<Building>().faction = faction;
        GameManager.Instance.buildings.Add(building);
        return building;
    }
}
