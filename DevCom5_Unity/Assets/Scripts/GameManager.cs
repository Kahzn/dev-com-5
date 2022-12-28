using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{

    public GameObject uiManagerPrefab = null;
    private GameObject uiManager = null;

    private List<GameObject> buildings = new();


    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Instantiate(uiManagerPrefab);
        commandInput = new CommandInput();
        commandInput.unitPrefabs = unitPrefabs;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
