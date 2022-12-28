using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static GameManager instance;

    public CommandInput commandInput = null;
    public UnitPrefabsScriptableObject prefabCollection;
    public Faction[] factions = new Faction[] { Faction.Bright, Faction.Dark, Faction.Gaia };
    public ResourceDepot[] resourceDepots = null;
    public List<GameObject> buildings = new();
    public ResourceManager resources
    {
        get
        {
            return resource_manager;
        }
    }

    private ResourceManager resource_manager = new();

    public GameManager()
    {
        resourceDepots = new ResourceDepot[Enum.GetValues(typeof(Faction)).Length];
        for (int i = 0; i < resourceDepots.Length; i++)
        {
            resourceDepots[i] = new ResourceDepot();
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        commandInput = new CommandInput();
        commandInput.unitPrefabs = prefabCollection;
    }

    public void RecalculatePopulationValues()
    {
        foreach (var faction in Enum.GetValues(typeof(Faction)))
        {
            int populationCapacity = 0;
            foreach (var building in buildings)
            {
                var housing = building.GetComponentInChildren<Housing>();
                if (housing != null)
                {
                    populationCapacity += housing.populationCapacityProvided;
                }                
            }
            var factionIndex = (int)faction;
            resourceDepots[factionIndex].PopulationCap = populationCapacity;
        }
    }

    public void Test()
    {
        var position = new Vector3(2, 0, 3);
        commandInput.BuildBuilding(BuildingType.TownCenter, Faction.Bright, position);
    }
}
