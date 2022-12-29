using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get; private set;
    }

    public CommandInput commandInput = null;
    public UnitPrefabsScriptableObject prefabCollection = null;
    public Faction[] factions = new Faction[] { Faction.Bright, Faction.Dark };
    public List<GameObject> buildings = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        commandInput = new CommandInput();
        commandInput.unitPrefabs = prefabCollection;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Test()
    {
        var position = new Vector3(2, 0, 3);
        commandInput.BuildBuilding(BuildingType.TownCenter, Faction.Bright, position);
    }
}
