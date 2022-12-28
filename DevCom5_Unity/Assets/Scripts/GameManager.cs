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
    public UnitPrefabsScriptableObject unitPrefabs = null;
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
        commandInput.unitPrefabs = unitPrefabs;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
